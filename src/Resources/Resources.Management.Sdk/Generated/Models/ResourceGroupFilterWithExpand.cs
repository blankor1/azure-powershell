// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is regenerated.

namespace Microsoft.Azure.Management.Resources.Models
{
    using System.Linq;

    public partial class ResourceGroupFilterWithExpand : ResourceGroupFilter
    {
        /// <summary>
        /// Initializes a new instance of the ResourceGroupFilterWithExpand class.
        /// </summary>
        public ResourceGroupFilterWithExpand()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the ResourceGroupFilterWithExpand class.
        /// </summary>

        /// <param name="tagName">The tag name.
        /// </param>

        /// <param name="tagValue">The tag value.
        /// </param>

        /// <param name="expand">Comma-separated list of additional properties to be included in the
        /// response. Valid values include createdTime, changedTime.
        /// </param>
        public ResourceGroupFilterWithExpand(string tagName = default(string), string tagValue = default(string), string expand = default(string))

        : base(tagName, tagValue)
        {
            this.Expand = expand;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();


        /// <summary>
        /// Gets or sets comma-separated list of additional properties to be included
        /// in the response. Valid values include createdTime, changedTime.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "$expand")]
        public string Expand {get; set; }
    }
}