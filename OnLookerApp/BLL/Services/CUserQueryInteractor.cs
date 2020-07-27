
using System;
using OnLooker.Core.Infrastructure;

namespace OnLooker.Core.Services
{
    public class CUserQueryInteractor : IDataInput
    {
        private IDataBaseIO _gates;
        private AParsingExpressions _parsingExpressions { get; set; }
        public CUserQueryInteractor(IDataBaseIO gates, AParsingExpressions parsingExpressions)      //todo: use a dependencyInjection
        {
            _gates = gates;
            _parsingExpressions = parsingExpressions;
        }

        public CReport PutRequest(QueryInfo dataInput)
        {
            CJob job = _gates.JobGateway.GetByQueryInfo(dataInput);
            CReport report = new CReport();
            if (job != null)
            {
                job.UpdateReports();   //it's happen to update db
                return job.Report;
            }
            else
            {
                CJob newJob = new CJob(dataInput, _parsingExpressions, _gates);
                report = newJob.CreateReport(dataInput);

            }
            //check for job existance
            //if job exists - > find get reports and update them
            //if not exists -> create job and push it work
            return report;
        }
    }
}
