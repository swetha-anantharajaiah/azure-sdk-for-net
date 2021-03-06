// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Management.WebSites.Models
{
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Newtonsoft.Json;
    using System.Linq;

    [Rest.Serialization.JsonTransformation]
    public partial class AzureActiveDirectoryRegistration : ProxyOnlyResource
    {
        /// <summary>
        /// Initializes a new instance of the AzureActiveDirectoryRegistration
        /// class.
        /// </summary>
        public AzureActiveDirectoryRegistration()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the AzureActiveDirectoryRegistration
        /// class.
        /// </summary>
        /// <param name="id">Resource Id.</param>
        /// <param name="name">Resource Name.</param>
        /// <param name="kind">Kind of resource.</param>
        /// <param name="type">Resource type.</param>
        public AzureActiveDirectoryRegistration(string id = default(string), string name = default(string), string kind = default(string), string type = default(string), string openIdIssuer = default(string), string clientId = default(string), string clientSecretSettingName = default(string), string clientSecretCertificateThumbprint = default(string))
            : base(id, name, kind, type)
        {
            OpenIdIssuer = openIdIssuer;
            ClientId = clientId;
            ClientSecretSettingName = clientSecretSettingName;
            ClientSecretCertificateThumbprint = clientSecretCertificateThumbprint;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "properties.openIdIssuer")]
        public string OpenIdIssuer { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "properties.clientId")]
        public string ClientId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "properties.clientSecretSettingName")]
        public string ClientSecretSettingName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "properties.clientSecretCertificateThumbprint")]
        public string ClientSecretCertificateThumbprint { get; set; }

    }
}
