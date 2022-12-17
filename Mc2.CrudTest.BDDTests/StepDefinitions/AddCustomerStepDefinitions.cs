using System;
using TechTalk.SpecFlow;

namespace Mc2.CrudTest.BDDTests.StepDefinitions
{
    [Binding]
    public class AddCustomerStepDefinitions
    {
        [When(@"the Steve James tries to register with email meysam@gmail\.com and")]
        public void WhenTheSteveJamesTriesToRegisterWithEmailMeysamGmail_ComAnd()
        {
            throw new PendingStepException();
        }

        [Then(@"the registration should fail with ""([^""]*)""")]
        public void ThenTheRegistrationShouldFailWith(string p0)
        {
            throw new PendingStepException();
        }
    }
}
