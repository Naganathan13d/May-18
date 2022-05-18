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


            

            Body[] bodies = workPart.Bodies.ToArray();
            Body b1 = bodies[0];
            Body b2 = bodies[1];

            TaggedObject[] taggedObject = new TaggedObject[1];

            NXObject nXObject = b2;

            taggedObject[0] = nXObject;


            BooleanFeature booleanFeature = null;
            BooleanBuilder booleanBuilder = workPart.Features.CreateBooleanBuilder(booleanFeature);

            booleanBuilder.Operation = Feature.BooleanType.Unite;

            booleanBuilder.Targets.Add(b1);

            //NXOpen.GeometricUtilities.BooleanRegionSelect booleanRegionSelect1;
            //booleanRegionSelect1 = booleanBuilder.BooleanRegionSelect;

            //booleanRegionSelect1.AssignTargets(taggedObject);

            BodyDumbRule bodyDumbRule1 = workPart.ScRuleFactory.CreateRuleBodyDumb(bodies, true);    

          

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
