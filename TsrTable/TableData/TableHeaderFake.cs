using System.Collections.Generic;
using TsrTable.Domain.Entities;

namespace TsrTable.C1RichTextBox.TableData
{
    public static class TableHeaderFake
    {
        public static List<TableHeaderEntity> GetData(int selector)
        {
            var result = new List<TableHeaderEntity>();
            if (selector == 0)
            {

                var entity = new TableHeaderEntity(1001, "トルクパターン", 1000, 0);
                result.Add(entity);

                var entity25 = new TableHeaderEntity(1101, "パターン2", 1000, 0);
                result.Add(entity25);




            }
            else if (selector == 1)
            {

                var entity2 = new TableHeaderEntity(20, "試験条件1\n応荷重", true, false, true);
                result.Add(entity2);
                var entity3 = new TableHeaderEntity(entity2, 21, "AW0", 1);
                result.Add(entity3);
                result.Add(new TableHeaderEntity(entity3, 211, "45%", 1));
                var entity31 = new TableHeaderEntity(entity2, 22, "AW3", 1);
                result.Add(entity31);
                result.Add(new TableHeaderEntity(entity31, 221, "45%", 1));
                result.Add(new TableHeaderEntity(entity31, 231, "75%", 1));

                var entity4 = new TableHeaderEntity(30, "試験条件2\n車輪径", true, false, true);
                result.Add(entity4);
                result.Add(new TableHeaderEntity(entity4, 311, "820", 1));
                result.Add(new TableHeaderEntity(entity4, 312, "860", 1));

                var entity5 = new TableHeaderEntity(40, "試験条件3\nFM(Hz)", true, false, true);
                result.Add(entity5);
                result.Add(new TableHeaderEntity(entity5, 411, "10", 1));
                result.Add(new TableHeaderEntity(entity5, 412, "20", 1));

                //var entity = new TableHeaderEntity(10, "トルクパターン", true, true, true);
                //result.Add(entity);
                //result.Add(new TableHeaderEntity(entity, 11, "基準値", 1));
                //result.Add(new TableHeaderEntity(entity, 12, "公差", 1));





            }
            else if (selector == 2)
            {
                var entity = new TableHeaderEntity(10, "title 3", false, true, false);
                result.Add(entity);

                var entity2 = new TableHeaderEntity(entity, 11, "1-1", 1);
                result.Add(entity2);

                result.Add(new TableHeaderEntity(entity2, 111, "1-1-1", 1));
                result.Add(new TableHeaderEntity(entity2, 112, "1-1-2", 1));

                result.Add(new TableHeaderEntity(entity, 12, "1-2", 1));
                result.Add(new TableHeaderEntity(entity, 13, "1-3", 1));
                result.Add(new TableHeaderEntity(entity, 14, "1-4", 1));
            }
            else if (selector == 3)
            {
                var entity = new TableHeaderEntity(10, "title 1", true, false, false);
                result.Add(entity);

                var entity3 = new TableHeaderEntity(entity, 11, "1-1", 1);
                result.Add(entity3);
                result.Add(new TableHeaderEntity(entity3, 111, "1-1-1", 1));
                result.Add(new TableHeaderEntity(entity3, 112, "1-1-2", 1));
                result.Add(new TableHeaderEntity(entity, 12, "1-2", 1));

                var entity2 = new TableHeaderEntity(20, "title 2", false, true, false);
                result.Add(entity2);
                result.Add(new TableHeaderEntity(entity2, 21, "2-1", 1));
                result.Add(new TableHeaderEntity(entity2, 22, "2-2", 1));
            }
            else
            {
                var entity = new TableHeaderEntity(10, "title 1", true, false, false);
                result.Add(entity);

                var entity3 = new TableHeaderEntity(entity, 11, "1-1", 1);
                result.Add(entity3);
                result.Add(new TableHeaderEntity(entity3, 111, "1-1-1", 1));
                result.Add(new TableHeaderEntity(entity3, 112, "1-1-2", 1));
                result.Add(new TableHeaderEntity(entity, 12, "1-2", 1));

                var entity2 = new TableHeaderEntity(20, "title 2", true, false, false);
                result.Add(entity2);
                result.Add(new TableHeaderEntity(entity2, 21, "2-1", 1));
                result.Add(new TableHeaderEntity(entity2, 22, "2-2", 1));

                var entity4 = new TableHeaderEntity(40, "title 4", true, true, false);
                result.Add(entity4);
                result.Add(new TableHeaderEntity(entity4, 41, "4-1", 1));
                result.Add(new TableHeaderEntity(entity4, 42, "4-2", 1));
            }
            return result;
        }
    }
}
