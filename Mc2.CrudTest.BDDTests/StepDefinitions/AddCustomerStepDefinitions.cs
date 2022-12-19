using System;
using TechTalk.SpecFlow;

namespace Mc2.CrudTest.BDDTests.StepDefinitions
{
    [Binding]
    public class AddCustomerStepDefinitions
    {
        [Given(@"I create a new customer \(Meysam,Amiri,(.*)(.*),Meysam@gmail\.com,(.*)\)")]
        public void GivenICreateANewCustomerMeysamAmiriMeysamGmail_Com(Decimal p0, Decimal p1, int p2)
        {
            throw new PendingStepException();
        }

        [Given(@"ModelState is correct")]
        public void GivenModelStateIsCorrect()
        {
            throw new PendingStepException();
        }

        [Then(@"the system should return <true>")]
        public void ThenTheSystemShouldReturnTrue()
        {
            throw new PendingStepException();
        }
    }
}
