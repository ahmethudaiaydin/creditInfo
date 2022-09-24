using System.Xml.Linq;
using System.Xml;

namespace CreditInfo.Test
{
    public class DataReadinessTests
    {
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