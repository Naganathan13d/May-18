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
            Point3d p3 = new Point3d(p1.X + (width/2), p1.Y + height, p1.Z);

            Point3d origin = new Point3d(0, 0, 0);
            // Comments added 

            Matrix3x3 matrix3X3 = workPart.WCS.CoordinateSystem.Orientation.Element;


            // To Check Tomorrow  

            DatumPlane datumPlane = workPart.Datums.CreateFixedDatumPlane(origin, matrix3X3);

           // Point3d endPoint = new Point3d(1, 0, 0);

           // DatumAxis datumAxis = workPart.Datums.CreateFixedDatumAxis(origin, endPoint);

           // Line l1 = workPart.Curves.CreateLine(p1, p2);
           // Line l2 = workPart.Curves.CreateLine(p2, p3);
           // Line l3 = workPart.Curves.CreateLine(p3, p1);

           // Sketch sketch = null;
           // SketchInPlaceBuilder sketchInPlaceBuilder = workPart.Sketches.CreateNewSketchInPlaceBuilder(sketch);
           // sketchInPlaceBuilder.PlaneOrFace.Value = datumPlane;
           //// sketchInPlaceBuilder.Axis.Value = datumAxis;
           // sketch = (Sketch)sketchInPlaceBuilder.Commit();
           // sketchInPlaceBuilder.Destroy();

           // sketch.Activate(Sketch.ViewReorient.True);

           // sketch.AddGeometry(l1, Sketch.InferConstraintsOption.InferCoincidentConstraints);
           // sketch.AddGeometry(l2, Sketch.InferConstraintsOption.InferCoincidentConstraints);
           // sketch.AddGeometry(l3, Sketch.InferConstraintsOption.InferCoincidentConstraints);

           // Sketch.ConstraintGeometry horizontalConstraint = new Sketch.ConstraintGeometry();
           // horizontalConstraint.Geometry = l1;
           // sketch.CreateHorizontalConstraint(horizontalConstraint);

           // Sketch.ConstraintGeometry verticalConstraint = new Sketch.ConstraintGeometry();
           // verticalConstraint.Geometry = l2;
           // sketch.CreateVerticalConstraint(verticalConstraint);

           // sketch.Deactivate(Sketch.ViewReorient.False, Sketch.UpdateLevel.Model);






        }



          
        public static int GetUnloadOption()
        {
            // Enumerator Data Type
            return 1;          
            
            
        }


    }
}
