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
            Session theSession = Session.GetSession();
            Part workPart = theSession.Parts.Work;

            double width = 20;
            double height = 40;

            Point3d p1 = new Point3d(5, 5, 0);
            Point3d p2 = new Point3d(p1.X + width, p1.Y, p1.Z);
            Point3d p3 = new Point3d(p1.X + (width / 2), p1.Y + height, p1.Z);

            Point3d origin = new Point3d(0, 0, 0);     

            Matrix3x3 matrix3X3 = workPart.WCS.CoordinateSystem.Orientation.Element;

            matrix3X3.Xx = 1;
            matrix3X3.Xy = 0;
            matrix3X3.Xz = 0;

            matrix3X3.Yx = 0;
            matrix3X3.Yy = 1;
            matrix3X3.Yz = 0;

            matrix3X3.Zx = 0;
            matrix3X3.Zy = 0;
            matrix3X3.Zz = 1;           

            DatumPlane datumPlane = workPart.Datums.CreateFixedDatumPlane(origin, matrix3X3);

            Point3d endPoint = new Point3d(1, 0, 0);

            DatumAxis datumAxis = workPart.Datums.CreateFixedDatumAxis(origin, endPoint);

            Line l1 = workPart.Curves.CreateLine(p1, p2);
            Line l2 = workPart.Curves.CreateLine(p2, p3);
            Line l3 = workPart.Curves.CreateLine(p3, p1);

            Sketch sketch = null;
            SketchInPlaceBuilder sketchInPlaceBuilder = workPart.Sketches.CreateNewSketchInPlaceBuilder(sketch);
            sketchInPlaceBuilder.PlaneOrFace.Value = datumPlane;

          
            sketch = (Sketch)sketchInPlaceBuilder.Commit();
            sketchInPlaceBuilder.Destroy();

            sketch.Activate(Sketch.ViewReorient.True);

            sketch.AddGeometry(l1, Sketch.InferConstraintsOption.InferCoincidentConstraints);
            sketch.AddGeometry(l2, Sketch.InferConstraintsOption.InferCoincidentConstraints);
            sketch.AddGeometry(l3, Sketch.InferConstraintsOption.InferCoincidentConstraints);

            Sketch.ConstraintGeometry horizontalConstraint = new Sketch.ConstraintGeometry();
            horizontalConstraint.Geometry = l1;
            sketch.CreateHorizontalConstraint(horizontalConstraint);

            Sketch.ConstraintGeometry verticalConstraint = new Sketch.ConstraintGeometry();
            verticalConstraint.Geometry = l2;
            sketch.CreateVerticalConstraint(verticalConstraint);


            Sketch.DimensionGeometry dimGeo1 = new Sketch.DimensionGeometry();
            dimGeo1.Geometry = l2;
            dimGeo1.AssocType = Sketch.AssocType.StartPoint;

            Sketch.DimensionGeometry dimGeo2 = new Sketch.DimensionGeometry();
            dimGeo2.Geometry = l2;
            dimGeo2.AssocType = Sketch.AssocType.EndPoint;

            Expression expheight = workPart.Expressions.CreateSystemExpression("newheight = 60");

            Point3d dim1place = new Point3d(p2.X + 25,p1.Y + (height/2),0);

            sketch.CreateDimension(Sketch.ConstraintType.VerticalDim, dimGeo1, dimGeo2, dim1place, expheight, Sketch.DimensionOption.CreateAsDriving);

            //Sketch.DimensionGeometry dimGeo3 = new Sketch.DimensionGeometry();
            //dimGeo3.Geometry = l1;
            //dimGeo3.AssocType = Sketch.AssocType.StartPoint;

            //Sketch.DimensionGeometry dimGeo4 = new Sketch.DimensionGeometry();
            //dimGeo4.Geometry = l1;
            //dimGeo4.AssocType = Sketch.AssocType.EndPoint;

            //Expression expwidth = workPart.Expressions.CreateSystemExpression("Width = 100");

            //Point3d dim2place = new Point3d(p2.X + 25, p1.Y + (width / 2), 0);


            //sketch.CreateDimension(Sketch.ConstraintType.HorizontalDim, dimGeo3, dimGeo4, dim2place, expwidth, Sketch.DimensionOption.CreateAsDriving);

            Sketch.DimensionGeometry dimGeo3 = new Sketch.DimensionGeometry();
            dimGeo3.Geometry = l1;

            Sketch.DimensionGeometry dimGeo4 = new Sketch.DimensionGeometry();
            dimGeo4.Geometry = l3;

            Expression expAngle = workPart.Expressions.CreateSystemExpression("Angle = 60");

            Point3d dim2place = new Point3d(p2.X + 25, p1.Y + (width / 2), 0);

            sketch.CreateDimension(Sketch.ConstraintType.AngularDim, dimGeo3, dimGeo4, dim2place, expAngle, Sketch.DimensionOption.CreateAsDriving);


            sketch.Deactivate(Sketch.ViewReorient.False, Sketch.UpdateLevel.Model);








        }




        public static int GetUnloadOption()
        {
            // Enumerator Data Type
            return 1;          
            
            
        }


    }
}
