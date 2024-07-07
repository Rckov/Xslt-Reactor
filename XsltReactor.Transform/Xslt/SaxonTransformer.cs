using System.Xml;

using XsltReactor.Transform.Xslt.Base;

namespace XsltReactor.Transform.Xslt;

internal class SaxonTransformer : BaseTransformer
{
   public override string Transform(XmlReader xml, XmlReader schema)
   {
      throw new NotImplementedException("Saxon");
   }
}