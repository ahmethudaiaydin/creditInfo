using System.Xml.Linq;
using System.Xml;
using CreditInfo.Service;
using CreditInfo.Domain;
using TanvirArjel.EFCore.GenericRepository;
using CreditInfo.Data;
using Moq;

namespace CreditInfo.Test
{
    public class UnitTest1
    {

        //Individual.DateOfBirth attribute value must be between 18 and 99 years
        //Contract.DateOfLastPayment attribute value must be before(in time) Contract.NextPaymentDate attribute value
        //Contract.DateAccountOpened attribute value must be before(in time) Contract.DateOfLastPayment attribute value
        //IndividualSum of(SubjectRole.GuaranteeAmount) attribute values must be lower than the Contract.OriginalAmount attribute value

        /// <summary>
        /// Time is up, therefore I couldn't continue to add other unit tests.
        /// </summary>
        [Fact]
        public async void Should_Return_Error_When_DateOfLastPayment_Is_After_NextPaymentDate()
        {
            var contract = new Contract
            {
                DateOfLastPayment = DateTime.Now,
                NextPaymentDate = DateTime.Now.AddDays(-1)
            };

            var mock = new Mock<IRepository<ContractContext>>();

            mock.Setup(x => x.InsertAsync(It.IsAny<ContractProcess>(), default)).ReturnsAsync(() => null);
            mock.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(() => 1);

            var contractValidator = new ContractValidator();
            ContractProcessService service = new(contractValidator, mock.Object);


            var result = await service.ProcessContract(contract);
            Assert.NotNull(result);
            Assert.Contains("Date Of Last Payment must be less than Next Payment Date", result.Errors);
        }


        /// <summary>
        /// I made couple of tests to take the XML file from url chunk by chunk. I am able to get it however, I couldn't find time to 
        /// map it into object. Therefore I remain it like that.
        /// </summary>
        [Fact]
        public async void Should_Read_Data_From_Url()
        {
            var url = "https://creditinfocandidate2k.z16.web.core.windows.net/assets/Sample.xml";

            XNamespace ns = "http://creditinfo.com/schemas/Sample/Data";
            var contractsInXML = (from el in FindElement(url) select el);

            //StringReader reader = new StringReader(rawContractData.ToString());
            //XmlSerializer xmlSerializer = new XmlSerializer(typeof(Contract), "http://creditinfo.com/schemas/Sample/Data");
            //try
            //{
            //    Contract contract = (Contract)xmlSerializer.Deserialize(reader);
            //}
            //catch (Exception)
            //{

            //    throw;
            //}


            //ContractProcessService service = new();

            //foreach (XElement contract in contractsInXML)
            //{
            //    await service.ProcessContract(contract);
            //}

        }

        static IEnumerable<XElement> FindElement(string url)
        {
            using (XmlReader xmlReader = XmlReader.Create(url))
            {
                xmlReader.MoveToContent();
                while (xmlReader.Read())
                {
                    switch (xmlReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (xmlReader.Name == "ci:Contract")
                            {
                                XElement? currentElement = XElement.ReadFrom(xmlReader) as XElement;
                                if (currentElement != null)
                                    yield return currentElement;
                            }
                            break;
                    }
                }
            }
        }
    }
}