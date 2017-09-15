using System;
using System.Collections.Generic;

using Microsoft.Practices.ServiceLocation;
using SolrNet;
using SolrNet.Commands.Parameters;
using SolrNet.Impl;

namespace SolrDemo
{
    internal class SolrNetApi
    {
        private static void Main(string[] args)
        {
            Startup.Init<SolrPolicyEntity>("http://139.198.13.12:7000/solr/PolicyCore");

            //Delete();

            //Add();

            Query();

            //FullDataImport();          

            //DeltaDataImport();         

            Console.WriteLine("操作已经完成，按任意键退出。");
            Console.ReadKey();
        }

        //删
        public static void Delete()
        {
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<SolrPolicyEntity>>();
            solr.Delete(SolrQuery.All);
            solr.Commit();
        }

        //增，改, 准实时数据导入
        public static void Add()
        {
            Random random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            var policyID = random.Next(10);

            var p = new SolrPolicyEntity
            {
                PolicyID = policyID,
                PolicyGroupID = 14,
                PolicyOperatorID = 55,
                PolicyOperatorName = "研发",
                PolicyCode = "23",
                PolicyName = string.Format("{0}_b国内始发大促销政策a", policyID),
                PolicyType = "OW/RT",
                TicketType = 1,
                FlightType = 10,
                DepartureDate = new DateTime(2017, 6, 8),
                ArrivalDate = new DateTime(2017, 9, 8),
                ReturnDepartureDate = new DateTime(2017, 6, 8),
                ReturnArrivalDate = new DateTime(2017, 9, 8),
                DepartureCityCodes = "/CN/",
                //TransitCityCodes = "",
                ArrivalCityCodes = "/UG/CV/GM/CG/GN/GH/GA/KE/LY/MU/MR/EG/MG/ML/SD/SO/MA/ZA/NE/NG/SN/ET/ER/AO/DZ/CM/TD/GQ/TZ/TN/CF/GW/CD/RW/SL/KM/LR/RE/TG/SC/CI/MZ/BF/BI/MW/ZW/BJ/D",
                OutTicketType = 0,
                OutTicketStart = new DateTime(2017, 6, 8),
                OutTicketEnd = new DateTime(2017, 9, 8),
                OutTicketPreDays = 0,
                Remark = "国内始发政策内部备注",
                Status = 2,
                SolrUpdatedTime = DateTime.Now.AddHours(-1)
            };

            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<SolrPolicyEntity>>();
            solr.Add(p);
            solr.Commit();
        }

        //查
        public static void Query()
        {
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<SolrPolicyEntity>>();

            SolrQueryResults<SolrPolicyEntity> results = null;

            int queryType = 14;            
            var queryOptions = new QueryOptions
            {
                StartOrCursor = new StartOrCursor.Start(0),
                Rows = 20
            };
            switch (queryType)
            {
                case 1:
                    //简单查询（模糊查询）
                    results = solr.Query(new SolrQuery("PolicyName:\"大促销\""), queryOptions);
                    break;               
                case 3:
                    //范围查询
                    results =
                        solr.Query(
                            new SolrQueryByRange<DateTime>("DepartureDate", new DateTime(2017, 4, 8),
                                new DateTime(2017, 4, 25)), queryOptions);
                    break;
                case 4:
                    //多值查询（模糊查询）
                    results = solr.Query(new SolrQueryInList("PolicyName", "国内始发", "川航前1普发"), queryOptions);
                    break;
                case 5:
                    //任意值查询
                    results = solr.Query(new SolrHasValueQuery("Remark"), queryOptions);
                    break;
                case 6:
                    //组合查询
                    results = solr.Query(new SolrQuery("PolicyName:\"大促销\"") && new SolrQuery("FlightType:10"),
                        queryOptions);
                    break;
                case 7:
                    //组合查询
                    results = solr.Query(new SolrQuery("PolicyName:\"普发\"") || new SolrQuery("FlightType:1"),
                        queryOptions);
                    break;
                case 8:
                    //组合查询
                    results = solr.Query(new SolrQuery("PolicyName:\"大促销\"") + new SolrQuery("FlightType:1"),
                        queryOptions);
                    break;
                case 9:
                    //组合查询
                    results = solr.Query(new SolrQuery("TicketType:1") - new SolrQuery("PolicyName:\"大促销\""),
                        queryOptions);
                    break;
                case 10:
                    //组合查询
                    results = solr.Query(new SolrQuery("TicketType:1") + !new SolrQuery("PolicyName:\"大促销\""),
                        queryOptions);
                    break;
                case 11:
                    //过滤查询
                    results = solr.Query(SolrQuery.All, new QueryOptions
                    {
                        FilterQueries = new ISolrQuery[] {                           
                            new SolrQuery("FlightType:10"),
                            new SolrQueryByRange<DateTime>("DepartureDate", new DateTime(2017, 6, 8), new DateTime(2017, 6, 8))
                        },
                        StartOrCursor = new StartOrCursor.Start(0),
                        Rows = 20
                    });
                    break;
                case 2: //字段查询 SolrQueryByField
                case 12:
                    //过滤查询
                    results = solr.Query(SolrQuery.All, new QueryOptions
                    {
                        FilterQueries = new QueryOptions().AddFilterQueries(
                                                     new SolrQueryByField("PolicyName", "大促销"),
                                                     //new SolrQueryByRange<DateTime>("DepartureDate", new DateTime(2017, 4, 8), new DateTime(2017, 6, 7))
                                                     new SolrQueryByRange<DateTime>("DepartureDate", new DateTime(2017, 4, 8), new DateTime(2017, 6, 8))
                                                    ).FilterQueries,
                        StartOrCursor = new StartOrCursor.Start(0),
                        Rows = 20
                    });
                    break;
                case 13:
                    //返回字段
                    results = solr.Query(SolrQuery.All, new QueryOptions
                    {
                        Fields = new[] { "PolicyName", "PolicyGroupID", "PolicyType", "TicketType", "FlightType", "DepartureDate", "Remark", "SolrUpdatedTime" },
                        StartOrCursor = new StartOrCursor.Start(0),
                        Rows = 20
                    });
                    break;
                case 14:
                    //排序
                    results = solr.Query(SolrQuery.All, new QueryOptions
                    {
                        OrderBy = new[] { new SortOrder("PolicyID", Order.DESC), SortOrder.Parse("SolrUpdatedTime ASC") },
                        StartOrCursor = new StartOrCursor.Start(0),
                        Rows = 20
                    });
                    break;
                case 15:
                    //分页：查询第2页的结果
                    results = solr.Query(SolrQuery.All, new QueryOptions
                    {
                        OrderBy = new[] { new SortOrder("PolicyID", Order.DESC) },
                        StartOrCursor = new StartOrCursor.Start(1),
                        Rows = 1
                    });
                    break;
                default:
                    //综合示例                    
                    results = solr.Query(SolrQuery.All, new QueryOptions
                    {
                        FilterQueries = new QueryOptions().AddFilterQueries(
                                                     new SolrQueryByField("PolicyName", "大促销"),
                                                     new SolrQueryByRange<DateTime>("DepartureDate", new DateTime(2017, 4, 8), new DateTime(2017, 6, 8))
                                                 ).FilterQueries,
                        Fields = new[] { "PolicyName", "PolicyGroupID", "PolicyType", "TicketType", "FlightType", "DepartureDate", "Remark", "SolrUpdatedTime" },
                        OrderBy = new[] { new SortOrder("PolicyID", Order.DESC), SortOrder.Parse("SolrUpdatedTime ASC") },
                        StartOrCursor = new StartOrCursor.Start(0),
                        Rows = 20
                    });
                    break;
            }

            Console.WriteLine("查询结果：");
            foreach (SolrPolicyEntity i in results)
            {
                Console.WriteLine("PolicyID：{0}，PolicyName：{1}，PolicyGroupID：{2}，PolicyType：{3}，TicketType：{4}，FlightType：{5}，DepartureDate：{6}，Remark：{7}，Solr更新时间：{8}",
                    i.PolicyID, i.PolicyName, i.PolicyGroupID, i.PolicyType, i.TicketType, i.FlightType, i.DepartureDate, i.Remark, i.SolrUpdatedTime);
            }
        }

        //全量数据导入
        public static void FullDataImport()
        {
            var conn = new SolrConnection("http://139.198.13.12:7000/solr/PolicyCore");
            string relativeUrl = "/dataimport";
            var parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("command", "full-import"),
                new KeyValuePair<string, string>("clean", "true"),
                new KeyValuePair<string, string>("commit", "true")
            };

            string result = conn.Get(relativeUrl, parameters);
            Console.WriteLine("结果：{0}", result);
        }

        //增量数据导入
        public static void DeltaDataImport()
        {
            var conn = new SolrConnection("http://139.198.13.12:7000/solr/PolicyCore");
            string relativeUrl = "/dataimport";
            var parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("command", "delta-import"),
                new KeyValuePair<string, string>("clean", "false"),
                new KeyValuePair<string, string>("commit", "true")
            };

            string result = conn.Get(relativeUrl, parameters);
            Console.WriteLine("结果：{0}", result);
        }
    }
}