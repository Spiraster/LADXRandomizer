using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using FileHelpers;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace ExcelParser
{
    public partial class Form1 : Form
    {
        private string[] ignoreList = new string[]
        {
            "OW1-1E-3", //pit to flooded cave
            "OW1-2A",   //manbo's cave
            "OW2-2A",
            "OW1-2E",   //exit from flooded cave
            "OW2-2E",
            "OW1-A0",   //village well
            "OW2-A0",
            "OW1-D9-2", //martha's bay SS
            "OW2-D9-2",
            "OW1-D9-3",
            "OW2-D9-3",
            "OW1-EA",   //fisherman's bridge
            "OW2-EA",
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string directory = "C:\\Users\\user\\Documents\\Visual Studio 2017\\Projects\\LADXRandomizer\\";

            SaveCSV(directory);

            var engine = new FileHelperEngine(typeof(WarpRecord));
            var warps = ((WarpRecord[])engine.ReadFile(directory + "warps" + ".csv")).Where(x => !ignoreList.Contains(x.Code)).ToArray();
            GenerateWarpsBlock(warps, directory + "warps");

            engine = new FileHelperEngine(typeof(ZoneRecord));
            var zones = (ZoneRecord[])engine.ReadFile(directory + "zones" + ".csv");
            GenerateZonesBlock(zones, directory + "zones");            
            
            try
            {
                File.Delete(directory + "warps.csv");
                File.Delete(directory + "zones.csv");
            }
            catch
            {
                MessageBox.Show("can't delete the things");
            }

            label_Done.Visible = true;
        }

        private void GenerateWarpsBlock(WarpRecord[] warps, string path)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\t\tprivate void Initialize()\n\t\t{\n");
            foreach (var record in warps.Where(x => x.Code.Contains("OW1")))
            {
                sb.Append("\t\t\tAdd(new WarpData(this)\n\t\t\t{\n");
                BuildWarpCode(record, sb);
                sb.Append("\t\t\t});\n");
            }

            foreach (var record in warps.Where(x => x.Code.Contains("OW2")))
            {
                sb.Append("\t\t\tAdd(new WarpData(this)\n\t\t\t{\n");
                BuildWarpCode(record, sb);
                sb.Append("\t\t\t});\n");
            }
            sb.Append("\t\t}\n\n");

            using (StreamWriter sw = new StreamWriter(path + ".txt"))
                sw.Write(sb.ToString());
        }

        private void GenerateZonesBlock(ZoneRecord[] zones, string path)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\t\tprivate void Initialize()\n\t\t{\n");
            foreach (var record in zones)
            {
                sb.Append("\t\t\tAdd(new ZoneData\n\t\t\t{\n");
                if (int.TryParse(record.Zone, out int num))
                {
                    sb.Append("\t\t\t\tZone = " + record.Zone + ",\n");

                    if (record.ZoneConnections != "")
                    {
                        sb.Append("\t\t\t\tZoneConnections = new ConnectionList\n\t\t\t\t{\n");
                        foreach (var zoneConnection in record.ZoneConnections.Split('|'))
                        {
                            var elements = zoneConnection.Split('_');
                            sb.Append("\t\t\t\t\tnew Connection(" + elements[0] + ", Connection.Outward");

                            if (elements.Length > 1)
                            {
                                sb.Append(", ");
                                var constraints = elements[1].Split('&');
                                foreach (var constraint in constraints)
                                    sb.Append(GetItem(constraint) + " | ");
                                sb.Remove(sb.Length - 3, 3);
                                sb.Replace(" | ,", ",");
                                sb.Replace(", ,", ",");
                            }
                            sb.Append("),\n");
                        }
                        sb.Append("\t\t\t\t},\n");
                    }

                    if (record.ItemConnections != "")
                    {
                        sb.Append("\t\t\t\tItemConnections = new ConnectionList\n\t\t\t\t{\n");
                        foreach (var itemConnection in record.ItemConnections.Split('|'))
                        {
                            var elements = itemConnection.Split('_');
                            sb.Append("\t\t\t\t\tnew Connection(\"" + elements[0] + "\", Connection.Outward");

                            if (elements.Length > 1)
                            {
                                sb.Append(", ");
                                var constraints = elements[1].Split('&');
                                foreach (var constraint in constraints)
                                    sb.Append(GetItem(constraint) + " | ");
                                sb.Remove(sb.Length - 3, 3);
                                sb.Replace(" | ,", ",");
                                sb.Replace(", ,", ",");
                            }
                            sb.Append("),\n");
                        }
                        sb.Append("\t\t\t\t},\n");
                    }

                    sb.Append("\t\t\t});\n");
                }
            }
            sb.Append("\t\t}\n");

            using (StreamWriter sw = new StreamWriter(path + ".txt"))
                sw.Write(sb.ToString());
        }

        private string BuildWarpCode(WarpRecord record, StringBuilder sb)
        {
            string location = "0x" + record.LocationValue.Replace(" ", "");
            location = location.Replace("0x0", "0xE");

            string def = "0x" + record.DefaultWarpValue.Replace(" ", "");
            def = def.Replace("0x0", "0xE");

            string description = record.Description;
            var split = record.Description.Split('-');
            if (split[0].Contains("OW"))
            {
                description = split[1].Substring(1);
            }

            sb.Append("\t\t\t\tCode = \"");
            sb.Append(record.Code);
            sb.Append("\",\n\t\t\t\tDescription = \"");
            sb.Append(description);
            sb.Append("\",\n\t\t\t\tWorld = ");
            sb.Append(record.World);
            sb.Append(",\n\t\t\t\tIndex = ");
            sb.Append(record.Index);

            if (record.Index2 != "")
            {
                sb.Append(",\n\t\t\t\tIndex2 = ");
                sb.Append(record.Index2);
            }

            sb.Append(",\n\t\t\t\tLocationValue = ");
            sb.Append(location);
            sb.Append(",\n\t\t\t\tDefaultWarpValue = ");
            sb.Append(def);
            sb.Append(",\n");

            if (record.DeadEnd != "")
            {
                sb.Append("\t\t\t\tDeadEnd = ");
                sb.Append(record.DeadEnd.ToLower());
                sb.Append(",\n");
            }

            if (record.Locked != "")
            {
                sb.Append("\t\t\t\tLocked = true,\n");
            }

            if (record.WarpConnectionsInward != "" || record.WarpConnectionsOutward != "")
            {
                sb.Append("\t\t\t\tWarpConnections = new ConnectionList\n\t\t\t\t{\n");

                if (record.WarpConnectionsInward != "")
                {
                    foreach (var connection in record.WarpConnectionsInward.Split('|'))
                    {
                        sb.Append("\t\t\t\t\tnew Connection(");

                        var elements = connection.Split('_');
                        sb.Append("\"" + elements[0] + "\", Connection.Inward");
                        if (elements.Length > 1)
                        {
                            sb.Append(", ");
                            var constraints = elements[1].Split('&');
                            foreach (var constraint in constraints)
                                sb.Append(GetItem(constraint) + " | ");
                            sb.Remove(sb.Length - 3, 3);
                            sb.Replace(" | ,", ",");
                            sb.Replace(", ,", ",");
                        }
                        sb.Append("),\n");
                    }
                }

                if (record.WarpConnectionsOutward != "")
                {
                    foreach (var connection in record.WarpConnectionsOutward.Split('|'))
                    {
                        sb.Append("\t\t\t\t\tnew Connection(");

                        var elements = connection.Split('_');
                        sb.Append("\"" + elements[0] + "\", Connection.Outward");
                        if (elements.Length > 1)
                        {
                            sb.Append(", ");
                            var constraints = elements[1].Split('&');
                            foreach (var constraint in constraints)
                                sb.Append(GetItem(constraint) + " | ");
                            sb.Remove(sb.Length - 3, 3);
                            sb.Replace(" | ,", ",");
                            sb.Replace(", ,", ",");
                        }
                        sb.Append("),\n");
                    }
                }

                sb.Append("\t\t\t\t},\n");
            }

            if (record.ZoneConnectionsInward != "" || record.ZoneConnectionsOutward != "")
            {
                sb.Append("\t\t\t\tZoneConnections = new ConnectionList\n\t\t\t\t{\n");

                if (record.ZoneConnectionsInward != "")
                {
                    foreach (var connection in record.ZoneConnectionsInward.Split('|'))
                    {
                        var elements = connection.Split('_');
                        int nul;
                        if (int.TryParse(elements[0], out nul))
                        {
                            sb.Append("\t\t\t\t\tnew Connection(");
                            sb.Append(elements[0] + ", Connection.Inward");
                            if (elements.Length > 1)
                            {
                                sb.Append(", ");
                                var constraints = elements[1].Split('&');
                                foreach (var constraint in constraints)
                                    sb.Append(GetItem(constraint) + " | ");
                                sb.Remove(sb.Length - 3, 3);
                                sb.Replace(" | ,", ",");
                                sb.Replace(", ,", ",");
                            }
                            sb.Append("),\n");
                        }
                    }
                }

                if (record.ZoneConnectionsOutward != "")
                {
                    foreach (var connection in record.ZoneConnectionsOutward.Split('|'))
                    {
                        var elements = connection.Split('_');
                        int nul;
                        if (int.TryParse(elements[0], out nul))
                        {
                            sb.Append("\t\t\t\t\tnew Connection(");
                            sb.Append(elements[0] + ", Connection.Outward");
                            if (elements.Length > 1)
                            {
                                sb.Append(", ");
                                var constraints = elements[1].Split('&');
                                foreach (var constraint in constraints)
                                    sb.Append(GetItem(constraint) + " | ");
                                sb.Remove(sb.Length - 3, 3);
                                sb.Replace(" | ,", ",");
                                sb.Replace(", ,", ",");
                            }
                            sb.Append("),\n");
                        }
                    }
                }

                sb.Append("\t\t\t\t}\n");
            }

            return sb.ToString();
        }

        private string GetItem(string input)
        {
            if (input.ToLower().Contains("waterfall") || input.ToLower().Contains("rooster") || input.ToLower().Contains("bowwow") || input.ToLower().Contains("kanaletswitch") || input.ToLower().Contains("scale") || input.ToLower().Contains("leaves"))
                return ", MiscFlags." + input.First().ToString().ToUpper() + input.Substring(1);
            else if (input.ToLower().Contains("key"))
                return ", Keys." + input.First().ToString().ToUpper() + input.Substring(1);
            else if (input.ToLower().Contains("song"))
                return ", Songs." + input.First().ToString().ToUpper() + input.Substring(1);
            else if (input.ToLower().Contains("instruments"))
                return ", Instruments.All";

            return "Items." + input.First().ToString().ToUpper() + input.Substring(1);
        }

        private void SaveCSV(string directory)
        {
            Microsoft.Office.Interop.Excel.Application xlApplication = new Microsoft.Office.Interop.Excel.Application();
            Workbooks xlWorkbooks = xlApplication.Workbooks;
            Workbook wb = xlWorkbooks.Open(directory + "warps" + ".xlsx");

            wb.Sheets[1].Activate();
            wb.SaveAs(directory + "warps" + ".csv", XlFileFormat.xlCSVWindows);

            wb.Sheets[2].Activate();
            wb.SaveAs(directory + "zones" + ".csv", XlFileFormat.xlCSVWindows);
            
            wb.Close(false);

            Marshal.FinalReleaseComObject(wb);
            Marshal.FinalReleaseComObject(xlWorkbooks);
            Marshal.FinalReleaseComObject(xlApplication);
        }
    }
}
