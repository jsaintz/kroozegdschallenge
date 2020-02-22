using Krooze.EntranceTest.WriteHere.Structure.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;

namespace Krooze.EntranceTest.WriteHere.Structure.Services
{
    public class XmlCruiseService
    {
        private const string _xmlPath = @"D:\projects\kroozegdschallrnge\Krooze.EntranceTest.WriteHere\Resources\Cruises.xml" ;
        XmlNode nodeRoot = null;
        XmlDocument xmlDocument = null;
        XmlNodeList listPax = null;
       
        public XmlCruiseService()
        {
            xmlDocument = new XmlDocument();
        }

        public CruiseDTO GetCruiseXml()
        {
            LoadAndSetXml();
            CruiseDTO cruiseDTO = ReturnCruiseValues();
            cruiseDTO.PassengerCruise = ReturnPassengerCruiseList();
            return cruiseDTO;
        }

        private void LoadAndSetXml()
        {
            xmlDocument.LoadXml(File.ReadAllText(_xmlPath));
            SetXmlNodesList();
        }

        private void SetXmlNodesList()
        {
            nodeRoot = xmlDocument.SelectSingleNode("Cruises");
            listPax = nodeRoot.SelectNodes("CategoryPriceDetails/Pax");
        }

        private List<PassengerCruiseDTO> ReturnPassengerCruiseList()
        {
            List<PassengerCruiseDTO> passengerCruiseList = new List<PassengerCruiseDTO>();
            foreach (XmlNode node in listPax)
            {
                PassengerCruiseDTO passengerCruiseDTO = new PassengerCruiseDTO
                {
                    PassengerCode = node.Attributes.GetNamedItem("PaxID").InnerText,
                    Cruise = new CruiseDTO
                    {
                        TotalValue = GetDecimalNodeValue("AllInclusivePerPax", node)
                    }
                };

                foreach (XmlNode nodeCharge in node.SelectNodes("Charge"))
                {
                    if (nodeCharge.Attributes.GetNamedItem("ChargeType").InnerText == "CAB")
                        passengerCruiseDTO.Cruise.CabinValue = GetDecimalNodeValue("GrossAmountBfrDisc", nodeCharge);

                    if (nodeCharge.Attributes.GetNamedItem("ChargeType").InnerText == "PCH")
                        passengerCruiseDTO.Cruise.PortCharge = GetDecimalNodeValue("GrossAmountBfrDisc", nodeCharge);
                }
                passengerCruiseList.Add(passengerCruiseDTO);
            }
            return passengerCruiseList;
        }

        private CruiseDTO ReturnCruiseValues()
        {
            CruiseDTO cruiseValues = new CruiseDTO
            {
                CruiseCode = GetStringNodeValue("CruiseId"),
                ShipName = GetStringNodeValue("ShipName"),
                CabinValue = GetDecimalNodeValue("CabinPrice", nodeRoot),
                PortCharge = GetDecimalNodeValue("PortChargesAmt", nodeRoot),
                TotalValue = GetDecimalNodeValue("TotalCabinPrice", nodeRoot)
            };

            return cruiseValues;
        }

        private decimal GetDecimalNodeValue(string nodeName, XmlNode node)
        {
            return Convert.ToDecimal(node.SelectSingleNode(nodeName).InnerText, CultureInfo.GetCultureInfo("en-US"));
        }

        private string GetStringNodeValue(string nodeName)
        {
            return nodeRoot.SelectSingleNode(nodeName).InnerText;
        }
    }
}
