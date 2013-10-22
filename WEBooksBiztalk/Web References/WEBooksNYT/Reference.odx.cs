namespace WEBooksBiztalk.WEBooksNYT.WEBooksNY_
{

    [Microsoft.XLANGs.BaseTypes.MessageTypeAttribute(
        Microsoft.XLANGs.BaseTypes.EXLangSAccess.ePublic,
        Microsoft.XLANGs.BaseTypes.EXLangSMessageInfo.eRequest,
        "WEBooksBiztalk.WEBooksNYT.WEBooksNY.LivrosTopSellers",
        new System.Type[]{
        },
        new string[]{
        },
        new System.Type[]{
        },
        -1,
        ""
    )]
    [System.SerializableAttribute]
    sealed public class LivrosTopSellers_request : Microsoft.BizTalk.XLANGs.BTXEngine.BTXMessage
    {

        private void __CreatePartWrappers()
        {
        }

        public LivrosTopSellers_request(string msgName, Microsoft.XLANGs.Core.Context ctx) : base(msgName, ctx)
        {
            __CreatePartWrappers();
        }
    }

    [System.SerializableAttribute]
    sealed public class __WEBooksBiztalk_WEBooksNYT_Reference_ArrayOfTopSellers__ : Microsoft.XLANGs.Core.XSDPart
    {
        private static WEBooksBiztalk.WEBooksNYT.Reference.ArrayOfTopSellers _schema = new WEBooksBiztalk.WEBooksNYT.Reference.ArrayOfTopSellers();

        public __WEBooksBiztalk_WEBooksNYT_Reference_ArrayOfTopSellers__(Microsoft.XLANGs.Core.XMessage msg, string name, int index) : base(msg, name, index) { }

        
        #region part reflection support
        public static Microsoft.XLANGs.BaseTypes.SchemaBase PartSchema { get { return (Microsoft.XLANGs.BaseTypes.SchemaBase)_schema; } }
        #endregion // part reflection support
    }

    [Microsoft.XLANGs.BaseTypes.MessageTypeAttribute(
        Microsoft.XLANGs.BaseTypes.EXLangSAccess.ePublic,
        Microsoft.XLANGs.BaseTypes.EXLangSMessageInfo.eResponse,
        "WEBooksBiztalk.WEBooksNYT.WEBooksNY.LivrosTopSellers",
        new System.Type[]{
            typeof(WEBooksBiztalk.WEBooksNYT.Reference.ArrayOfTopSellers)
        },
        new string[]{
            "LivrosTopSellersResult"
        },
        new System.Type[]{
            typeof(__WEBooksBiztalk_WEBooksNYT_Reference_ArrayOfTopSellers__)
        },
        0,
        @"http://WEBooksNY.pt/#ArrayOfTopSellers"
    )]
    [System.SerializableAttribute]
    sealed public class LivrosTopSellers_response : Microsoft.BizTalk.XLANGs.BTXEngine.BTXMessage
    {
        public __WEBooksBiztalk_WEBooksNYT_Reference_ArrayOfTopSellers__ LivrosTopSellersResult;

        private void __CreatePartWrappers()
        {
            LivrosTopSellersResult = new __WEBooksBiztalk_WEBooksNYT_Reference_ArrayOfTopSellers__(this, "LivrosTopSellersResult", 0);
            this.AddPart("LivrosTopSellersResult", 0, LivrosTopSellersResult);
        }

        public LivrosTopSellers_response(string msgName, Microsoft.XLANGs.Core.Context ctx) : base(msgName, ctx)
        {
            __CreatePartWrappers();
        }
    }

    [System.SerializableAttribute]
    sealed public class __WEBooksBiztalk_WEBooksNYT_Reference_ArrayOfDesconto__ : Microsoft.XLANGs.Core.XSDPart
    {
        private static WEBooksBiztalk.WEBooksNYT.Reference.ArrayOfDesconto _schema = new WEBooksBiztalk.WEBooksNYT.Reference.ArrayOfDesconto();

        public __WEBooksBiztalk_WEBooksNYT_Reference_ArrayOfDesconto__(Microsoft.XLANGs.Core.XMessage msg, string name, int index) : base(msg, name, index) { }

        
        #region part reflection support
        public static Microsoft.XLANGs.BaseTypes.SchemaBase PartSchema { get { return (Microsoft.XLANGs.BaseTypes.SchemaBase)_schema; } }
        #endregion // part reflection support
    }

    [Microsoft.XLANGs.BaseTypes.MessageTypeAttribute(
        Microsoft.XLANGs.BaseTypes.EXLangSAccess.ePublic,
        Microsoft.XLANGs.BaseTypes.EXLangSMessageInfo.eRequest,
        "WEBooksBiztalk.WEBooksNYT.WEBooksNY.AssinalaLivrosDesconto",
        new System.Type[]{
            typeof(WEBooksBiztalk.WEBooksNYT.Reference.ArrayOfDesconto)
        },
        new string[]{
            "listaLivros"
        },
        new System.Type[]{
            typeof(__WEBooksBiztalk_WEBooksNYT_Reference_ArrayOfDesconto__)
        },
        0,
        @"http://WEBooksNY.pt/#ArrayOfDesconto"
    )]
    [System.SerializableAttribute]
    sealed public class AssinalaLivrosDesconto_request : Microsoft.BizTalk.XLANGs.BTXEngine.BTXMessage
    {
        public __WEBooksBiztalk_WEBooksNYT_Reference_ArrayOfDesconto__ listaLivros;

        private void __CreatePartWrappers()
        {
            listaLivros = new __WEBooksBiztalk_WEBooksNYT_Reference_ArrayOfDesconto__(this, "listaLivros", 0);
            this.AddPart("listaLivros", 0, listaLivros);
        }

        public AssinalaLivrosDesconto_request(string msgName, Microsoft.XLANGs.Core.Context ctx) : base(msgName, ctx)
        {
            __CreatePartWrappers();
        }
    }

    [Microsoft.XLANGs.BaseTypes.MessageTypeAttribute(
        Microsoft.XLANGs.BaseTypes.EXLangSAccess.ePublic,
        Microsoft.XLANGs.BaseTypes.EXLangSMessageInfo.eResponse,
        "WEBooksBiztalk.WEBooksNYT.WEBooksNY.AssinalaLivrosDesconto",
        new System.Type[]{
            typeof(WEBooksBiztalk.WEBooksNYT.Reference.ArrayOfDesconto)
        },
        new string[]{
            "AssinalaLivrosDescontoResult"
        },
        new System.Type[]{
            typeof(__WEBooksBiztalk_WEBooksNYT_Reference_ArrayOfDesconto__)
        },
        0,
        @"http://WEBooksNY.pt/#ArrayOfDesconto"
    )]
    [System.SerializableAttribute]
    sealed public class AssinalaLivrosDesconto_response : Microsoft.BizTalk.XLANGs.BTXEngine.BTXMessage
    {
        public __WEBooksBiztalk_WEBooksNYT_Reference_ArrayOfDesconto__ AssinalaLivrosDescontoResult;

        private void __CreatePartWrappers()
        {
            AssinalaLivrosDescontoResult = new __WEBooksBiztalk_WEBooksNYT_Reference_ArrayOfDesconto__(this, "AssinalaLivrosDescontoResult", 0);
            this.AddPart("AssinalaLivrosDescontoResult", 0, AssinalaLivrosDescontoResult);
        }

        public AssinalaLivrosDesconto_response(string msgName, Microsoft.XLANGs.Core.Context ctx) : base(msgName, ctx)
        {
            __CreatePartWrappers();
        }
    }

    [Microsoft.XLANGs.BaseTypes.PortTypeOperationAttribute(
        "AssinalaLivrosDesconto",
        new System.Type[]{
            typeof(WEBooksBiztalk.WEBooksNYT.WEBooksNY_.AssinalaLivrosDesconto_request), 
            typeof(WEBooksBiztalk.WEBooksNYT.WEBooksNY_.AssinalaLivrosDesconto_response)
        },
        new string[]{
        }
    )]
    [Microsoft.XLANGs.BaseTypes.PortTypeOperationAttribute(
        "LivrosTopSellers",
        new System.Type[]{
            typeof(WEBooksBiztalk.WEBooksNYT.WEBooksNY_.LivrosTopSellers_request), 
            typeof(WEBooksBiztalk.WEBooksNYT.WEBooksNY_.LivrosTopSellers_response)
        },
        new string[]{
        }
    )]
    [Microsoft.XLANGs.BaseTypes.PortTypeAttribute(Microsoft.XLANGs.BaseTypes.EXLangSAccess.ePublic, "")]
    [Microsoft.XLANGs.BaseTypes.WSDLProxyNameAttribute(typeof(WEBooksBiztalk.WEBooksNYT.WEBooksNY))]
    [System.SerializableAttribute]
    sealed public class WEBooksNY : Microsoft.BizTalk.XLANGs.BTXEngine.BTXPortBase
    {
        public WEBooksNY(int portInfo, Microsoft.XLANGs.Core.IServiceProxy s)
            : base(portInfo, s)
        { }
        public WEBooksNY(WEBooksNY p)
            : base(p)
        { }

        public override Microsoft.XLANGs.Core.PortBase Clone()
        {
            WEBooksNY p = new WEBooksNY(this);
            return p;
        }

        public static readonly Microsoft.XLANGs.BaseTypes.EXLangSAccess __access = Microsoft.XLANGs.BaseTypes.EXLangSAccess.ePublic;
        #region port reflection support
        static public Microsoft.XLANGs.Core.OperationInfo AssinalaLivrosDesconto = new Microsoft.XLANGs.Core.OperationInfo
        (
            "AssinalaLivrosDesconto",
            System.Web.Services.Description.OperationFlow.RequestResponse,
            typeof(WEBooksNY),
            typeof(AssinalaLivrosDesconto_request),
            typeof(AssinalaLivrosDesconto_response),
            new System.Type[]{},
            new string[]{}
        );
        static public Microsoft.XLANGs.Core.OperationInfo LivrosTopSellers = new Microsoft.XLANGs.Core.OperationInfo
        (
            "LivrosTopSellers",
            System.Web.Services.Description.OperationFlow.RequestResponse,
            typeof(WEBooksNY),
            typeof(LivrosTopSellers_request),
            typeof(LivrosTopSellers_response),
            new System.Type[]{},
            new string[]{}
        );
        static public System.Collections.Hashtable OperationsInformation
        {
            get
            {
                System.Collections.Hashtable h = new System.Collections.Hashtable();
                h[ "AssinalaLivrosDesconto" ] = AssinalaLivrosDesconto;
                h[ "LivrosTopSellers" ] = LivrosTopSellers;
                return h;
            }
        }
        #endregion // port reflection support
    }

    [Microsoft.XLANGs.BaseTypes.BPELExportableAttribute(true)]
    [Microsoft.XLANGs.BaseTypes.TargetXmlNamespaceAttribute("http://WEBooksNY.pt/")]
    sealed public class _MODULE_PROXY_ { }
}
