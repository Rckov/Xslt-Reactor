using System.Xml;

namespace XsltReactor.Transform.Interfaces;

public interface ITransform
{
   XsltUriResolver? XsltUriResolver { get; set; }

   Task<string> TransformAsync(XmlReader xml, XmlReader schema);
}