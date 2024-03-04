using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace SITemplate.Core.Utils
{
    public class XmlParser
    {

        /// <summary>
        /// Xml 저장. 폴더가 없다면 알아서 만들어줌
        /// </summary>
        /// <typeparam name="T">저장할 클래스 타입</typeparam>
        /// <param name="data">저장할 클래스</param>
        /// <param name="folderPath">폴더 경로</param>
        /// <param name="fileName">파일명 (확장자 포함)</param>
        public static void SaveXml<T>(T data, string folderPath, string fileFullPath) where T : class
        {
            if (!Directory.Exists(folderPath)) { _ = Directory.CreateDirectory(folderPath); }

            XmlSerializer ser = new XmlSerializer(typeof(T));
            using (StreamWriter sw = new StreamWriter(fileFullPath))
            {
                ser.Serialize(sw, data);
            }
        }

        /// <summary>
        /// Xml 불러오기. 만약 파일이 존재하지 않는다면 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileFullPath"></param>
        /// <returns></returns>
        public static T LoadXml<T>(string fileFullPath) where T : class, new()
        {
            return File.Exists(fileFullPath) ? PopulateSetting<T>(fileFullPath) : new T();
        }

        public static T PopulateSetting<T>(string fileFullPath)
        {
            T reVal;
            var ser = new XmlSerializer(typeof(T));
            using (var reader = XmlReader.Create(fileFullPath))
            {
                reVal = (T)ser.Deserialize(reader);
            }

            return reVal;
        }
    }
}