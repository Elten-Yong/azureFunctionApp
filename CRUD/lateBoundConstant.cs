// *********************************************************************
// Created by : Latebound Constants Generator 1.2021.12.1 for XrmToolBox
// Author     : Jonas Rapp https://jonasr.app/
// GitHub     : https://github.com/rappen/LCG-UDG/
// Source Org : https://org2a7db823.crm5.dynamics.com
// Filename   : C:\Users\User\Desktop\lateBoundConstant.cs
// Created    : 2022-01-03 14:14:12
// *********************************************************************

namespace App.Custom
{
    /// <summary>OwnershipType: UserOwned, IntroducedVersion: 5.0.0.0</summary>
    /// <remarks>Business that represents a customer or potential customer. The company that is billed in business transactions.</remarks>
    public static class Account
    {
        public const string EntityName = "account";
        public const string EntityCollectionName = "accounts";

        #region Attributes

        /// <summary>Type: Uniqueidentifier, RequiredLevel: SystemRequired</summary>
        /// <remarks>Unique identifier of the account.</remarks>
        public const string PrimaryKey = "accountid";
        /// <summary>Type: String, RequiredLevel: ApplicationRequired, MaxLength: 160, Format: Text</summary>
        /// <remarks>Type the company or business name.</remarks>
        public const string PrimaryName = "name";

        #endregion Attributes
    }
}


/***** LCG-configuration-BEGIN *****\
<?xml version="1.0" encoding="utf-16"?>
<Settings xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <Version>1.2021.12.1</Version>
  <NameSpace>App.Custom</NameSpace>
  <UseCommonFile>true</UseCommonFile>
  <SaveConfigurationInCommonFile>true</SaveConfigurationInCommonFile>
  <FileName>DisplayName</FileName>
  <ConstantName>DisplayName</ConstantName>
  <ConstantCamelCased>false</ConstantCamelCased>
  <DoStripPrefix>false</DoStripPrefix>
  <StripPrefix>_</StripPrefix>
  <XmlProperties>true</XmlProperties>
  <XmlDescription>true</XmlDescription>
  <Regions>true</Regions>
  <RelationShips>true</RelationShips>
  <RelationshipLabels>false</RelationshipLabels>
  <OptionSets>true</OptionSets>
  <GlobalOptionSets>false</GlobalOptionSets>
  <Legend>false</Legend>
  <CommonAttributes>None</CommonAttributes>
  <AttributeSortMode>None</AttributeSortMode>
  <SelectedEntities>
    <SelectedEntity>
      <Name>account</Name>
      <Attributes>
        <string>accountid</string>
        <string>name</string>
      </Attributes>
      <Relationships />
    </SelectedEntity>
  </SelectedEntities>
</Settings>
\***** LCG-configuration-END   *****/
