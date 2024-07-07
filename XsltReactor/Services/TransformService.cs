using System.Xml;
using System.Xml.Linq;

using XsltReactor.Transform;
using XsltReactor.Transform.Enums;

namespace XsltReactor.Services;

internal class TransformService
{
   private readonly Transformer _transformer;

   public TransformService()
   {
      _transformer = new Transformer();
      _transformer.Create(EngineType.XslCompiledTransform);
   }

   public async Task<string?> Transform(string xslText, string xmlText)
   {
      try
      {
         var xmlReader = GetReader(xmlText);
         var xslReader = GetReader(xslText);

         return await _transformer.TransformAsync(xslReader, xmlReader);
      }
      catch (Exception ex)
      {
         return ex.Message;
      }
   }

   private XmlReader GetReader(string xmlText)
   {
      return XElement.Parse(xmlText).CreateReader();
   }
}