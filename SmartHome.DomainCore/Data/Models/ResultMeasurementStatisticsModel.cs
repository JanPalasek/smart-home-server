using System.Collections.Generic;

namespace SmartHome.DomainCore.Data.Models
{
    public class ResultMeasurementStatisticsModel
    {
        public ResultMeasurementStatisticsModel(IList<AggregatedStatisticsModel> insideResult, IList<AggregatedStatisticsModel> outsideResult)
        {
            InsideResult = insideResult;
            OutsideResult = outsideResult;
        }

        public IList<AggregatedStatisticsModel> InsideResult { get; }
        public IList<AggregatedStatisticsModel> OutsideResult { get; }
    }
}