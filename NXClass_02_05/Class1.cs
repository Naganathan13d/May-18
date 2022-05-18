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

            Body[] bodies1 = new Body[2];
            Body body2 = (Body)workPart.Bodies.FindObject("EXTRUDE(1)");
            Body body3 = (Body)workPart.Bodies.FindObject("EXTRUDE(3)");
            bodies1[0] = body2;
            bodies1[1] = body3;

            
            Body b1 = (Body)workPart.Bodies.FindObject("EXTRUDE(2)");
                  


            BooleanFeature booleanFeature = null;
            BooleanBuilder booleanBuilder = workPart.Features.CreateBooleanBuilder(booleanFeature);

            booleanBuilder.Operation = Feature.BooleanType.Unite;

            booleanBuilder.Targets.Add(b1);          

            BodyDumbRule bodyDumbRule1 = workPart.ScRuleFactory.CreateRuleBodyDumb(bodies1, true);           

            SelectionIntentRule[] rules1 = new SelectionIntentRule[1];

            ScCollector scCollector2 = workPart.ScCollectors.CreateCollector();
            rules1[0] = bodyDumbRule1;
            scCollector2.ReplaceRules(rules1, false);

            booleanBuilder.ToolBodyCollector = scCollector2;


            booleanBuilder.Commit();
            booleanBuilder.Destroy();







            //Guide.CreateSphere(double0, double01, double02, double03);






        }







        public static int GetUnloadOption()
        {
            // Enumerator Data Type
            return 1;          
            
            
        }


    }
}
