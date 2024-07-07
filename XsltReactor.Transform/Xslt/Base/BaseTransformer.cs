using System.Xml;

using XsltReactor.Transform.Interfaces;

namespace XsltReactor.Transform.Xslt.Base;

internal abstract class BaseTransformer : ITransform
{
   public XsltUriResolver? XsltUriResolver { get; set; }

   public abstract string Transform(XmlReader xml, XmlReader schema);

   public Task<string> TransformAsync(XmlReader xml, XmlReader schema)
   {
      return Task.Run(() => Transform(xml, schema));
   }
}