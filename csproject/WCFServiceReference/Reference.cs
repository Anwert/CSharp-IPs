//------------------------------------------------------------------------------
// <автоматически создаваемое>
//     Этот код создан программой.
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторного создания кода.
// </автоматически создаваемое>
//------------------------------------------------------------------------------

namespace IPsWCF.Models
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.3")]
    [System.Runtime.Serialization.DataContractAttribute(Name="IPList", Namespace="http://schemas.datacontract.org/2004/07/IPsWCF.Models")]
    public partial class IPList : object
    {
        
        private IPsWCF.Models.IP[] DataField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public IPsWCF.Models.IP[] Data
        {
            get
            {
                return this.DataField;
            }
            set
            {
                this.DataField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.3")]
    [System.Runtime.Serialization.DataContractAttribute(Name="IP", Namespace="http://schemas.datacontract.org/2004/07/IPsWCF.Models")]
    public partial class IP : object
    {
        
        private string AddressField;
        
        private string IdField;
        
        private int MaskField;
        
        private string SubnetField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Address
        {
            get
            {
                return this.AddressField;
            }
            set
            {
                this.AddressField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Mask
        {
            get
            {
                return this.MaskField;
            }
            set
            {
                this.MaskField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Subnet
        {
            get
            {
                return this.SubnetField;
            }
            set
            {
                this.SubnetField = value;
            }
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.3")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName="IIPsRepositoryWCF")]
public interface IIPsRepositoryWCF
{
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IIPsRepositoryWCF/GetIPList", ReplyAction="http://tempuri.org/IIPsRepositoryWCF/GetIPListResponse")]
    System.Threading.Tasks.Task<GetIPListResponse> GetIPListAsync(GetIPListRequest request);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IIPsRepositoryWCF/CreateIP", ReplyAction="http://tempuri.org/IIPsRepositoryWCF/CreateIPResponse")]
    System.Threading.Tasks.Task<CreateIPResponse> CreateIPAsync(CreateIPRequest request);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IIPsRepositoryWCF/DeleteIP", ReplyAction="http://tempuri.org/IIPsRepositoryWCF/DeleteIPResponse")]
    System.Threading.Tasks.Task DeleteIPAsync(string id);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IIPsRepositoryWCF/UpdateIP", ReplyAction="http://tempuri.org/IIPsRepositoryWCF/UpdateIPResponse")]
    System.Threading.Tasks.Task<UpdateIPResponse> UpdateIPAsync(UpdateIPRequest request);
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.3")]
[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
[System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
public partial class GetIPListRequest
{
    
    [System.ServiceModel.MessageBodyMemberAttribute(Name="GetIPList", Namespace="http://tempuri.org/", Order=0)]
    public GetIPListRequestBody Body;
    
    public GetIPListRequest()
    {
    }
    
    public GetIPListRequest(GetIPListRequestBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.3")]
[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
[System.Runtime.Serialization.DataContractAttribute()]
public partial class GetIPListRequestBody
{
    
    public GetIPListRequestBody()
    {
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.3")]
[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
[System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
public partial class GetIPListResponse
{
    
    [System.ServiceModel.MessageBodyMemberAttribute(Name="GetIPListResponse", Namespace="http://tempuri.org/", Order=0)]
    public GetIPListResponseBody Body;
    
    public GetIPListResponse()
    {
    }
    
    public GetIPListResponse(GetIPListResponseBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.3")]
[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
[System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
public partial class GetIPListResponseBody
{
    
    [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
    public IPsWCF.Models.IPList GetIPListResult;
    
    public GetIPListResponseBody()
    {
    }
    
    public GetIPListResponseBody(IPsWCF.Models.IPList GetIPListResult)
    {
        this.GetIPListResult = GetIPListResult;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.3")]
[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
[System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
public partial class CreateIPRequest
{
    
    [System.ServiceModel.MessageBodyMemberAttribute(Name="CreateIP", Namespace="http://tempuri.org/", Order=0)]
    public CreateIPRequestBody Body;
    
    public CreateIPRequest()
    {
    }
    
    public CreateIPRequest(CreateIPRequestBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.3")]
[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
[System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
public partial class CreateIPRequestBody
{
    
    [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
    public IPsWCF.Models.IP ip;
    
    public CreateIPRequestBody()
    {
    }
    
    public CreateIPRequestBody(IPsWCF.Models.IP ip)
    {
        this.ip = ip;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.3")]
[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
[System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
public partial class CreateIPResponse
{
    
    [System.ServiceModel.MessageBodyMemberAttribute(Name="CreateIPResponse", Namespace="http://tempuri.org/", Order=0)]
    public CreateIPResponseBody Body;
    
    public CreateIPResponse()
    {
    }
    
    public CreateIPResponse(CreateIPResponseBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.3")]
[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
[System.Runtime.Serialization.DataContractAttribute()]
public partial class CreateIPResponseBody
{
    
    public CreateIPResponseBody()
    {
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.3")]
[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
[System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
public partial class UpdateIPRequest
{
    
    [System.ServiceModel.MessageBodyMemberAttribute(Name="UpdateIP", Namespace="http://tempuri.org/", Order=0)]
    public UpdateIPRequestBody Body;
    
    public UpdateIPRequest()
    {
    }
    
    public UpdateIPRequest(UpdateIPRequestBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.3")]
[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
[System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
public partial class UpdateIPRequestBody
{
    
    [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
    public IPsWCF.Models.IP ip;
    
    public UpdateIPRequestBody()
    {
    }
    
    public UpdateIPRequestBody(IPsWCF.Models.IP ip)
    {
        this.ip = ip;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.3")]
[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
[System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
public partial class UpdateIPResponse
{
    
    [System.ServiceModel.MessageBodyMemberAttribute(Name="UpdateIPResponse", Namespace="http://tempuri.org/", Order=0)]
    public UpdateIPResponseBody Body;
    
    public UpdateIPResponse()
    {
    }
    
    public UpdateIPResponse(UpdateIPResponseBody Body)
    {
        this.Body = Body;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.3")]
[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
[System.Runtime.Serialization.DataContractAttribute()]
public partial class UpdateIPResponseBody
{
    
    public UpdateIPResponseBody()
    {
    }
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.3")]
public interface IIPsRepositoryWCFChannel : IIPsRepositoryWCF, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.3")]
public partial class IPsRepositoryWCFClient : System.ServiceModel.ClientBase<IIPsRepositoryWCF>, IIPsRepositoryWCF
{
    
    /// <summary>
    /// Реализуйте этот разделяемый метод для настройки конечной точки службы.
    /// </summary>
    /// <param name="serviceEndpoint">Настраиваемая конечная точка</param>
    /// <param name="clientCredentials">Учетные данные клиента.</param>
    static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
    
    public IPsRepositoryWCFClient() : 
            base(IPsRepositoryWCFClient.GetDefaultBinding(), IPsRepositoryWCFClient.GetDefaultEndpointAddress())
    {
        this.Endpoint.Name = EndpointConfiguration.BasicHttpBinding_IIPsRepositoryWCF.ToString();
        ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
    }
    
    public IPsRepositoryWCFClient(EndpointConfiguration endpointConfiguration) : 
            base(IPsRepositoryWCFClient.GetBindingForEndpoint(endpointConfiguration), IPsRepositoryWCFClient.GetEndpointAddress(endpointConfiguration))
    {
        this.Endpoint.Name = endpointConfiguration.ToString();
        ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
    }
    
    public IPsRepositoryWCFClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
            base(IPsRepositoryWCFClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
    {
        this.Endpoint.Name = endpointConfiguration.ToString();
        ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
    }
    
    public IPsRepositoryWCFClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(IPsRepositoryWCFClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
    {
        this.Endpoint.Name = endpointConfiguration.ToString();
        ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
    }
    
    public IPsRepositoryWCFClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(binding, remoteAddress)
    {
    }
    
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    System.Threading.Tasks.Task<GetIPListResponse> IIPsRepositoryWCF.GetIPListAsync(GetIPListRequest request)
    {
        return base.Channel.GetIPListAsync(request);
    }
    
    public System.Threading.Tasks.Task<GetIPListResponse> GetIPListAsync()
    {
        GetIPListRequest inValue = new GetIPListRequest();
        inValue.Body = new GetIPListRequestBody();
        return ((IIPsRepositoryWCF)(this)).GetIPListAsync(inValue);
    }
    
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    System.Threading.Tasks.Task<CreateIPResponse> IIPsRepositoryWCF.CreateIPAsync(CreateIPRequest request)
    {
        return base.Channel.CreateIPAsync(request);
    }
    
    public System.Threading.Tasks.Task<CreateIPResponse> CreateIPAsync(IPsWCF.Models.IP ip)
    {
        CreateIPRequest inValue = new CreateIPRequest();
        inValue.Body = new CreateIPRequestBody();
        inValue.Body.ip = ip;
        return ((IIPsRepositoryWCF)(this)).CreateIPAsync(inValue);
    }
    
    public System.Threading.Tasks.Task DeleteIPAsync(string id)
    {
        return base.Channel.DeleteIPAsync(id);
    }
    
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    System.Threading.Tasks.Task<UpdateIPResponse> IIPsRepositoryWCF.UpdateIPAsync(UpdateIPRequest request)
    {
        return base.Channel.UpdateIPAsync(request);
    }
    
    public System.Threading.Tasks.Task<UpdateIPResponse> UpdateIPAsync(IPsWCF.Models.IP ip)
    {
        UpdateIPRequest inValue = new UpdateIPRequest();
        inValue.Body = new UpdateIPRequestBody();
        inValue.Body.ip = ip;
        return ((IIPsRepositoryWCF)(this)).UpdateIPAsync(inValue);
    }
    
    public virtual System.Threading.Tasks.Task OpenAsync()
    {
        return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
    }
    
    public virtual System.Threading.Tasks.Task CloseAsync()
    {
        return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
    }
    
    private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
    {
        if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IIPsRepositoryWCF))
        {
            System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
            result.MaxBufferSize = int.MaxValue;
            result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
            result.MaxReceivedMessageSize = int.MaxValue;
            result.AllowCookies = true;
            return result;
        }
        throw new System.InvalidOperationException(string.Format("Не удалось найти конечную точку с именем \"{0}\".", endpointConfiguration));
    }
    
    private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
    {
        if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IIPsRepositoryWCF))
        {
            return new System.ServiceModel.EndpointAddress("http://localhost:5001/IPsRepository.svc");
        }
        throw new System.InvalidOperationException(string.Format("Не удалось найти конечную точку с именем \"{0}\".", endpointConfiguration));
    }
    
    private static System.ServiceModel.Channels.Binding GetDefaultBinding()
    {
        return IPsRepositoryWCFClient.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_IIPsRepositoryWCF);
    }
    
    private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
    {
        return IPsRepositoryWCFClient.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_IIPsRepositoryWCF);
    }
    
    public enum EndpointConfiguration
    {
        
        BasicHttpBinding_IIPsRepositoryWCF,
    }
}
