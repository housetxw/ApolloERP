using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Ae.BaoYang.Service.Common.Exceptions;

namespace Ae.BaoYang.Service.Common.Common
{
    public static class XmlHelper
    {
        /// <summary>
        /// Deserializes xml string to entity.
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="xml">Xml string to deserialize.</param>
        /// <returns>Deserialized entity.</returns>
        public static TEntity Deserialize<TEntity>(string xml)
            where TEntity : class
        {
            ParameterChecker.CheckNullOrEmpty(xml, "xml");

            using (var xmlReader = new StringReader(xml))
            {
                try
                {
                    return new XmlSerializer(typeof(TEntity)).Deserialize(xmlReader) as TEntity;
                }
                catch (Exception ex)
                {
                    throw new XmlSerializeException(ex, xml);
                }
            }
        }


        public class XmlSerializeException : CustomException
        {
            public XmlSerializeException(Exception innerException, string xml)
                : base(string.Format("{0} xml:{1}", innerException.Message, xml), innerException)
            {
            }
        }
    }
}
