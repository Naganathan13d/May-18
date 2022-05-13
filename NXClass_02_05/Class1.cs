using NXOpen;
using NXOpen.Features;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXClass_02_05
{
    public class Class1
    {
        public static void Main()
        {

            Session session = Session.GetSession();
            Part workpart = session.Parts.Work;

            Sketch newsketch = null;
            SketchInPlaceBuilder skt = workpart.Sketches.CreateSketchInPlaceBuilder2(newsketch);
            skt.OriginOption = OriginMethod.WorkPartOrigin;

            Point3d plnpt = new Point3d(0, 0, 0);
            Vector3d plvctr = new Vector3d(1, 0, 0);
            Plane plane1 = workpart.Planes.CreatePlane(plnpt, plvctr, SmartObject.UpdateOption.WithinModeling);
            skt.PlaneReference = plane1;

            Point3d axpt = new Point3d(0, 0, 0);
            Vector3d axvctr = new Vector3d(0, 1, 0);
            Direction Dir1 = workpart.Directions.CreateDirection(axpt, axvctr, SmartObject.UpdateOption.WithinModeling);
            skt.AxisReference = Dir1;


            newsketch = (Sketch)skt.Commit();
            newsketch.SetName("Table1");
            newsketch.Activate(Sketch.ViewReorient.False);

            Point3d pt1 = new Point3d(0, 0, 0);
            Point3d pt2 = new Point3d(0, 0, 10);
            Point3d pt3 = new Point3d(0, 10, 10);
            Point3d pt4 = new Point3d(0, 10, 0);

            Line l1 = workpart.Curves.CreateLine(pt1, pt2);
            Line l2 = workpart.Curves.CreateLine(pt2, pt3);
            Line l3 = workpart.Curves.CreateLine(pt3, pt4);
            Line l4 = workpart.Curves.CreateLine(pt4, pt1);

            newsketch.AddGeometry(l1);
            newsketch.AddGeometry(l2);
            newsketch.AddGeometry(l3);
            newsketch.AddGeometry(l4);

            skt.Destroy();

            Sketch.ConstraintGeometry vr1 = new Sketch.ConstraintGeometry();
            vr1.Geometry = l1;
            newsketch.CreateVerticalConstraint(vr1);

            Sketch.ConstraintGeometry hr1 = new Sketch.ConstraintGeometry();
            hr1.Geometry = l2;
            newsketch.CreateHorizontalConstraint(hr1);

            Sketch.ConstraintGeometry vr2 = new Sketch.ConstraintGeometry();
            vr2.Geometry = l3;
            newsketch.CreateVerticalConstraint(vr2);

            Sketch.ConstraintGeometry hr2 = new Sketch.ConstraintGeometry();
            hr2.Geometry = l4;
            newsketch.CreateHorizontalConstraint(hr2);

            Expression exp1 = workpart.Expressions.Create("Length1=10");

            Sketch.DimensionGeometry Dl1 = new Sketch.DimensionGeometry();
            Dl1.Geometry = l1;
            Dl1.AssocType = Sketch.AssocType.StartPoint;

            Sketch.DimensionGeometry Dl2 = new Sketch.DimensionGeometry();
            Dl2.Geometry = l1;
            Dl2.AssocType = Sketch.AssocType.EndPoint;

            Point3d dimloc = new Point3d(0, 2.5, 1);

            newsketch.CreateDimension(Sketch.ConstraintType.VerticalDim, Dl1, Dl2, dimloc, exp1);

            newsketch.Deactivate(Sketch.ViewReorient.False, Sketch.UpdateLevel.SketchOnly);

            Extrude ext = null;
            ExtrudeBuilder extbuild = workpart.Features.CreateExtrudeBuilder(ext);

            Curve[] curves = new Curve[4];
            curves[0] = l1;
            curves[1] = l2;
            curves[2] = l3;
            curves[3] = l4;

            SelectionIntentRule[] recrulearray = new SelectionIntentRule[1];
            SelectionIntentRule Selection1 = workpart.ScRuleFactory.CreateRuleCurveDumb(curves);
            recrulearray[0] = Selection1;

            Section extsection = workpart.Sections.CreateSection();
            extsection.AddToSection(recrulearray, l1, null, null, new Point3d(0, 0, 0), Section.Mode.Create);
            extbuild.Section = extsection;

            extbuild.Direction = workpart.Directions.CreateDirection(new Point3d(0, 0, 0), new Vector3d(1, 0, 0), SmartObject.UpdateOption.WithinModeling);

            extbuild.Limits.StartExtend.TrimType = NXOpen.GeometricUtilities.Extend.ExtendType.Value;
            extbuild.Limits.StartExtend.SetValue("0");

            extbuild.Limits.EndExtend.TrimType = NXOpen.GeometricUtilities.Extend.ExtendType.Value;
            extbuild.Limits.EndExtend.SetValue("20");

            extbuild.BooleanOperation.Type = NXOpen.GeometricUtilities.BooleanOperation.BooleanType.Create;

            extbuild.Commit();
            extbuild.Destroy();







        }




        public static int GetUnloadOption()
        {
            // Enumerator Data Type
            return 1;          
            
            
        }


    }
}
