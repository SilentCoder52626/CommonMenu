using DomainModule.Dto;
using DomainModule.Entity;
using DomainModule.Exceptions;
using DomainModule.RepositoryInterface;
using DomainModule.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModule.Service
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentRepository _attachmentRepo;

        public AttachmentService(IUnitOfWork unitOfWork, IAttachmentRepository attachmentRepo)
        {
            _unitOfWork = unitOfWork;
            _attachmentRepo = attachmentRepo;
        }

        public int Create(AttachmentCreateDto dto)
        {
            try
            {
                using var tx = _unitOfWork.BeginTransaction();
                Attachment Attachment = CreateAttachment(dto);
                tx.Commit();
                return Attachment.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private Attachment CreateAttachment(AttachmentCreateDto dto)
        {
            var Attachment = new Attachment()
            {
                FileName = dto.FileName,
                Path = dto.Path,
                UploadedBy = dto.UploadedBy,
                UploadedDateTime = DateTime.Now
            };
            _attachmentRepo.Insert(Attachment);

            _unitOfWork.Complete();
            return Attachment;
        }

        public int CreateWihoutTransaction(AttachmentCreateDto dto)
        {
            Attachment Attachment = CreateAttachment(dto);
            return Attachment.Id;
        }

        public bool Delete(int id)
        {
            try
            {
                using var tx = _unitOfWork.BeginTransaction();

                var Attachment = _attachmentRepo.GetById(id) ?? throw new CustomException("Attachment not found.");
                _attachmentRepo.Delete(Attachment);
                _unitOfWork.Complete();
                tx.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool DeleteWithoutTransasction(int id)
        {
            try
            {

                var Attachment = _attachmentRepo.GetById(id) ?? throw new CustomException("Attachment not found.");
                _attachmentRepo.Delete(Attachment);
                _unitOfWork.Complete();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
