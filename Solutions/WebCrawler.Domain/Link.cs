using SharpArch.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler.Domain
{
    public class Link : Entity
    {
        private Uri _fromUrl;
        private Uri _toUrl;

        protected  Link()
        {
            
        }

        public Link(Uri fromUrl, Uri toUrl)
        {
            _fromUrl = fromUrl;
            _toUrl = toUrl;
        }

        [DomainSignature]
        public virtual Uri FromUrl
        {
            get { return _fromUrl; }
            protected set { _fromUrl = value; }
        }

        [DomainSignature]
        public virtual Uri ToUrl
        {
            get { return _toUrl; }
            protected set { _toUrl = value; }
        }
    }
}
