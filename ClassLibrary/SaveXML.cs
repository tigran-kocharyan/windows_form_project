using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace ClassLibrary
{
    /// <summary>
    /// Класс для считывания и записи в XML-файл.
    /// </summary>
    public class SaveXML
    {
        /// <summary>
        /// Метод для считывания xml информации.
        /// </summary>
        /// <returns></returns>
        public static Unit[] ReadXML()
        {
            Unit[] units = new Unit[2];

            XmlDocument xml = new XmlDocument();
            xml.Load("../../../saves/autosave.xml");

            var game = xml["Game"];

            var hero = game["Hero"];

            // Обычное чтение значений из XML-файла через взятие значений из ячеек Node-ов.

            units[0] = new Unit(hero["Name"].InnerText.ToString(), double.Parse(hero["DPS"].InnerText),
                double.Parse(hero["HDPS"].InnerText), double.Parse(hero["SingleDPS"].InnerText),
                double.Parse(hero["Life"].InnerText), double.Parse(hero["Reload"].InnerText));

            var enemy = game["Enemy"];
            units[1] = new Unit(enemy["Name"].InnerText.ToString(), double.Parse(enemy["DPS"].InnerText),
                double.Parse(enemy["HDPS"].InnerText), double.Parse(enemy["SingleDPS"].InnerText),
                double.Parse(enemy["Life"].InnerText), double.Parse(enemy["Reload"].InnerText));

            return units;
        }


        /// <summary>
        /// Метод для быстрой записи всей информации в .xml файл -
        /// Для последующего считывания.
        /// </summary>
        /// <param name="hero"></param>
        /// <param name="enemy"></param>
        public static void WriteXML(Unit hero, Unit enemy)
        {
            string documentation = $"<?xml version=\"1.0\" encoding=\"utf-8\" ?>";

            // Можно заметить, что если в поле хранится значение со знаком "<" или ">"
            // То программа обработает это как "&lt;" и "&gt;" соответственно
            // Во избежании ошибок при чтении.
            string heroInfo =
                "<Hero>" +
                $"<Name>{hero.Name.ToString().Replace("<", "&lt;").Replace(">", "&gt;")}</Name>" +
                $"<DPS>{hero.DPS.ToString().Replace("<", "&lt;").Replace(">", "&gt;")}</DPS>" +
                $"<HDPS>{hero.HDPS.ToString().Replace("<", "&lt;").Replace(">", "&gt;")}</HDPS>" +
                $"<Life>{hero.Life.ToString().Replace("<", "&lt;").Replace(">", "&gt;")}</Life>" +
                $"<SingleDPS>{hero.SingleDPS.ToString().Replace("<", "&lt;").Replace(">", "&gt;")}</SingleDPS>" +
                $"<Reload>{hero.Reload.ToString().Replace("<", "&lt;").Replace(">", "&gt;")}</Reload>" +
                $"</Hero>";

            // Здесь так же можно заметить, что если в поле хранится значение со знаком "<" или ">"
            // То программа обработает это как "&lt;" и "&gt;" соответственно
            // Во избежании ошибок при чтении.
            string EnemyInfo =
                "<Enemy>" +
                $"<Name>{enemy.Name.ToString().Replace("<", "&lt;").Replace(">", "&gt;")}</Name>" +
                $"<DPS>{enemy.DPS.ToString().Replace("<", "&lt;").Replace(">", "&gt;")}</DPS>" +
                $"<HDPS>{enemy.HDPS.ToString().Replace("<", "&lt;").Replace(">", "&gt;")}</HDPS>" +
                $"<Life>{enemy.Life.ToString().Replace("<", "&lt;").Replace(">", "&gt;")}</Life>" +
                $"<SingleDPS>{enemy.SingleDPS.ToString().Replace("<", "&lt;").Replace(">", "&gt;")}</SingleDPS>" +
                $"<Reload>{enemy.Reload.ToString().Replace("<", "&lt;").Replace(">", "&gt;")}</Reload>" +
                $"</Enemy>";

            // Информация соединяется и записывается в xml документ для последующего чтения.
            string info = documentation + "<Game>"+ heroInfo + EnemyInfo + "</Game>";
            File.WriteAllText("../../../saves/autosave.xml", info);
        }
    }
}
