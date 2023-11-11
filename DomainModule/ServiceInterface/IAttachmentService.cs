using DomainModule.Dto;
using Org.BouncyCastle.Asn1.BC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModule.ServiceInterface
{
    public interface IAttachmentService
    {
        int Create(AttachmentCreateDto dto);
        int CreateWihoutTransaction(AttachmentCreateDto dto);
        bool Delete(int id);
    }
}
