
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Interop.Common;


namespace CHXQ.XMManager
{
    class ZoomPoint
    {
        public static  void DrawCircle(Point3d ZoomPoint)
        {
            DocumentLock docLock = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.LockDocument();
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;
            Entity entity = null;
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                LayerTable acLyrTbl = acTrans.GetObject(acCurDb.LayerTableId, OpenMode.ForRead) as LayerTable;
                string strLayerName = "Zoom";
                LayerTableRecord acLyrTblRec = new LayerTableRecord();

                if (acLyrTbl.Has(strLayerName) == false)
                {
                    acLyrTblRec.Color = Autodesk.AutoCAD.Colors.Color.FromRgb(255, 255, 255);
                    acLyrTblRec.Name = strLayerName;
                    acLyrTbl.UpgradeOpen();
                    acLyrTbl.Add(acLyrTblRec);
                    acTrans.AddNewlyCreatedDBObject(acLyrTblRec, true);
                }
                else
                {
                    TypedValue[] glq = new TypedValue[]
                {
                    new TypedValue((int)DxfCode.LayerName,strLayerName)
                };
                    SelectionFilter sf = new SelectionFilter(glq);
                    PromptSelectionResult SS = acDoc.Editor.SelectAll(sf);
                    Autodesk.AutoCAD.EditorInput.SelectionSet SSet = SS.Value;
                    if (SSet != null)
                        foreach (ObjectId id in SSet.GetObjectIds())
                        {

                            AcadObject Adobj = null;
                            entity = (Entity)acTrans.GetObject(id, OpenMode.ForWrite, true);
                            DBObject obj = (DBObject)entity;
                            Adobj = (AcadObject)obj.AcadObject;
                            Adobj.Delete();
                        }

                }

                BlockTable acBlkTbl;
                BlockTableRecord acBlkTblRec;
                acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead) as BlockTable;
                acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                Circle acCirc = new Circle();
                acCirc.SetDatabaseDefaults();
                acCirc.Center = ZoomPoint;
                acCirc.Radius = 0.25;
                acCirc.Layer = strLayerName;

                acBlkTblRec.AppendEntity(acCirc);
                acTrans.AddNewlyCreatedDBObject(acCirc, true);

                ObjectIdCollection collection = new ObjectIdCollection();
                collection.Add(acCirc.ObjectId);

                Hatch hatch = new Hatch();
                hatch.HatchStyle = HatchStyle.Normal;
                hatch.Layer = strLayerName;
                hatch.Color = Autodesk.AutoCAD.Colors.Color.FromRgb(0, 0, 255);
                hatch.SetHatchPattern(HatchPatternType.PreDefined, "SOLID");
                hatch.AppendLoop(HatchLoopTypes.Default, collection);
                hatch.EvaluateHatch(true);
                acBlkTblRec.AppendEntity(hatch);
                acTrans.AddNewlyCreatedDBObject(hatch, true);

                docLock.Dispose();
                acTrans.Commit();
            }
        }
        public static void Zoom(Point3d pMin, Point3d pMax, Point3d pCenter, double dFactor)
        {
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            int nCurVport = System.Convert.ToInt32(Application.GetSystemVariable("CVPORT"));

            if (acCurDb.TileMode == true)
            {
                if (pMin.Equals(new Point3d()) == true && pMax.Equals(new Point3d()) == true)
                {
                    pMin = acCurDb.Extmin;
                    pMax = acCurDb.Extmax;
                }
            }
            else
            {
                if (nCurVport == 1)
                {
                    if (pMin.Equals(new Point3d()) == true && pMax.Equals(new Point3d()) == true)
                    {
                        pMin = acCurDb.Pextmin;
                        pMax = acCurDb.Pextmax;
                    }
                }
                else
                {
                    if (pMin.Equals(new Point3d()) == true && pMax.Equals(new Point3d()) == true)
                    {
                        pMin = acCurDb.Extmin;
                        pMax = acCurDb.Extmax;
                    }
                }
            }
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                using (ViewTableRecord acView = acDoc.Editor.GetCurrentView())
                {

                    Extents3d eExtents;
                    Matrix3d matWCS2DCS;
                    matWCS2DCS = Matrix3d.PlaneToWorld(acView.ViewDirection);
                    matWCS2DCS = Matrix3d.Displacement(acView.Target - Point3d.Origin) * matWCS2DCS;
                    matWCS2DCS = Matrix3d.Rotation(-acView.ViewTwist, acView.ViewDirection, acView.Target) * matWCS2DCS;

                    if (pCenter.DistanceTo(Point3d.Origin) != 0)
                    {
                        pMin = new Point3d(pCenter.X - (acView.Width / 2), pCenter.Y - (acView.Height / 2), 0);
                        pMax = new Point3d(pCenter.X + (acView.Width / 2), pCenter.Y + (acView.Height / 2), 0);
                    }
                    using (Line acLine = new Line(pMin, pMax))
                    {
                        eExtents = new Extents3d(new Point3d(pMax.X-10,pMax.Y,pMax.Z),new Point3d(pMin.X+10,pMin.Y,pMin.Z));

                    }
                    double dViewRatio;
                    dViewRatio = (acView.Width / acView.Height);

                    matWCS2DCS = matWCS2DCS.Inverse();
                    eExtents.TransformBy(matWCS2DCS);

                    double dWidth;
                    double dHeight;
                    Point2d pNewCentPt;

                    if (pCenter.DistanceTo(Point3d.Origin) != 0)
                    {
                        dWidth = acView.Width;
                        dHeight = acView.Height;
                        if (dFactor == 0)
                        {
                            pCenter = pCenter.TransformBy(matWCS2DCS);
                        }
                        pNewCentPt = new Point2d(pCenter.X, pCenter.Y);
                    }
                    else
                    {
                        dWidth = eExtents.MaxPoint.X - eExtents.MinPoint.X;
                        dHeight = eExtents.MaxPoint.Y - eExtents.MinPoint.Y;

                        pNewCentPt = new Point2d(((eExtents.MaxPoint.X + eExtents.MinPoint.X) * 0.5),
                                                ((eExtents.MinPoint.Y + eExtents.MinPoint.Y) * 0.5));
                    }
                    if (dWidth > (dHeight * dViewRatio))
                        dHeight = dWidth / dViewRatio;
                    if (dFactor != 0)
                    {
                        acView.Height = dHeight * dFactor;
                        acView.Width = dWidth * dFactor;
                    }

                    acView.CenterPoint = pNewCentPt;

                    acDoc.Editor.SetCurrentView(acView);
                }
                acTrans.Commit();
            }
        }

        public static void TXZoom(Point3d pMin, Point3d pMax, Point3d pCenter, double dFactor)
        {
            //  获得当前文档和数据库   Get the current document and database
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            int nCurVport = System.Convert.ToInt32(Application.GetSystemVariable("CVPORT"));

            // Get the extents of the current space no points 
            // or only a center point is provided
            // 检查当前是否是模型空间  Check to see if Model space is current
            if (acCurDb.TileMode == true)
            {
                if (pMin.Equals(new Point3d()) == true &&
                    pMax.Equals(new Point3d()) == true)
                {
                    pMin = acCurDb.Extmin;
                    pMax = acCurDb.Extmax;
                }
            }
            else
            {
                // 检查当前是否是图纸空间  Check to see if Paper space is current
                if (nCurVport == 1)
                {
                    // Get the extents of Paper space
                    if (pMin.Equals(new Point3d()) == true &&
                        pMax.Equals(new Point3d()) == true)
                    {
                        pMin = acCurDb.Pextmin;
                        pMax = acCurDb.Pextmax;
                    }
                }
                else
                {
                    // 获得模型空间的范围  Get the extents of Model space
                    if (pMin.Equals(new Point3d()) == true &&
                        pMax.Equals(new Point3d()) == true)
                    {
                        pMin = acCurDb.Extmin;
                        pMax = acCurDb.Extmax;
                    }
                }
            }

            // 启动一个事务  Start a transaction
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                // Get the current view
                using (ViewTableRecord acView = acDoc.Editor.GetCurrentView())
                {
                    Extents3d eExtents;

                    // Translate WCS coordinates to DCS
                    Matrix3d matWCS2DCS;
                    matWCS2DCS = Matrix3d.PlaneToWorld(acView.ViewDirection);
                    matWCS2DCS = Matrix3d.Displacement(acView.Target - Point3d.Origin) * matWCS2DCS;
                    matWCS2DCS = Matrix3d.Rotation(-acView.ViewTwist,
                                                   acView.ViewDirection,
                                                   acView.Target) * matWCS2DCS;

                    // If a center point is specified, define the min and max 
                    // point of the extents
                    // for Center and Scale modes
                    if (pCenter.DistanceTo(Point3d.Origin) != 0)
                    {
                        pMin = new Point3d(pCenter.X - (acView.Width / 2),
                                           pCenter.Y - (acView.Height / 2), 0);

                        pMax = new Point3d((acView.Width / 2) + pCenter.X,
                                           (acView.Height / 2) + pCenter.Y, 0);
                    }

                    // 使用一个直线创建一个范围对象 译者注：此处可能有错误，因为直线只有GeometricExtents属性表示范围  Create an extents object using a line
                    using (Line acLine = new Line(pMin, pMax))
                    {
                        eExtents = new Extents3d(pMin,
                                                 pMax);
                    }

                    // 计算当前视图的宽度与高度的比率  Calculate the ratio between the width and height of the current view
                    double dViewRatio;
                    dViewRatio = (acView.Width / acView.Height);

                    // 转换视图的范围  Tranform the extents of the view
                    matWCS2DCS = matWCS2DCS.Inverse();
                    eExtents.TransformBy(matWCS2DCS);

                    double dWidth;
                    double dHeight;
                    Point2d pNewCentPt;

                    // 检查中心点是否已提供（中心和缩放模式）  Check to see if a center point was provided (Center and Scale modes)
                    if (pCenter.DistanceTo(Point3d.Origin) != 0)
                    {
                        dWidth = acView.Width;
                        dHeight = acView.Height;

                        if (dFactor == 0)
                        {
                            pCenter = pCenter.TransformBy(matWCS2DCS);
                        }

                        pNewCentPt = new Point2d(pCenter.X, pCenter.Y);
                    }
                    else // 配合窗口、范围和界限模式计算当前视图新的宽度和高度  Working in Window, Extents and Limits mode
                    {
                        // Calculate the new width and height of the current view
                        dWidth = eExtents.MaxPoint.X - eExtents.MinPoint.X;
                        dHeight = eExtents.MaxPoint.Y - eExtents.MinPoint.Y;

                        // 获得视图的中心点  Get the center of the view
                        pNewCentPt = new Point2d(((eExtents.MaxPoint.X + eExtents.MinPoint.X) * 0.5),
                                                 ((eExtents.MaxPoint.Y + eExtents.MinPoint.Y) * 0.5));
                    }

                    // 检查新的宽度是否适合当前窗口 Check to see if the new width fits in current window
                    if (dWidth > (dHeight * dViewRatio)) dHeight = dWidth / dViewRatio;

                    // 调整大小并缩放视图  Resize and scale the view
                    if (dFactor != 0)
                    {
                        acView.Height = dHeight* dFactor;
                        acView.Width = dWidth* dFactor;

                    }

                    // Set the center of the view
                    acView.CenterPoint = pNewCentPt;

                    // Set the current view
                    acDoc.Editor.SetCurrentView(acView);
                }

                // Commit the changes
                acTrans.Commit();
            }
        }
    }
}
