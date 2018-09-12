namespace WebCrawler.Domain
{
    using SharpArch.Domain.DomainModel;
    using System;

    public class CrawlerSettings : ValueObject
    {
       
        [DomainSignature]
        public virtual int SearchDepth { get; protected set; }
        [DomainSignature]
        public virtual long MaxMemoryConsumption { get; protected set; }
        [DomainSignature]
        public virtual long BatchSize { get; protected set; }

        public CrawlerSettings()
        {
                
        }

        public CrawlerSettings(int searchDepth, long maxMemoryConsumption, long batchSize)
        {
            ChangeSettings(searchDepth, maxMemoryConsumption, batchSize);

        }

        public CrawlerSettings ChangeSettings(int searchDepth, long maxMemoryConsumption, long batchSize)
        {
            SearchDepth = searchDepth;
            MaxMemoryConsumption = maxMemoryConsumption;
            BatchSize = batchSize;
            return this;
        }
    
    }
}