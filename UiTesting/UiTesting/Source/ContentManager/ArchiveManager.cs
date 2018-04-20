using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiTesting.Source.Content
{
    public class ArchiveManager : ContentManager
    {
        #region Fields

        private string p_ArchivePath = null;
        //private ZipFile p_Archive = null;
        private bool p_UseArchive = false;

        #endregion

        #region Properties

        public virtual string ArchivePath { get { return p_ArchivePath; } }

        public bool UseArchive { get { return p_UseArchive; } set { p_UseArchive = value; } }

        #endregion

        #region Constructors

        public ArchiveManager(IServiceProvider serviceProvider) : this(serviceProvider, null) { }

        public ArchiveManager(IServiceProvider serviceProvider, string archive) : base(serviceProvider)
        {
            if(archive != null)
            {
                //this.p_Archive = ZipFile.Read(archive);
                p_ArchivePath = archive;
                p_UseArchive = true;
            }
        }

        #endregion
    }
}
