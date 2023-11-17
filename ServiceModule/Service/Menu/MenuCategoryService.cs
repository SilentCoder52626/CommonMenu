using DomainModule.Dto.Menu;
using DomainModule.Entity.Menu;
using DomainModule.Enums;
using DomainModule.Exceptions;
using DomainModule.RepositoryInterface;
using DomainModule.RepositoryInterface.Menu;
using DomainModule.ServiceInterface;
using DomainModule.ServiceInterface.Menu;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModule.Service.Menu
{
    public class MenuCategoryService : IMenuCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMenuCategoryRepository _menuCategoryRepo;
        private readonly IAttachmentService _attachmentService;
        private readonly IAttachmentRepository _attachmentRepo;
        private readonly IMenuCategoryImagesRepository _categoryImageRepo;

        public MenuCategoryService(IUnitOfWork unitOfWork, IMenuCategoryRepository menuCategoryRepository, IAttachmentService attachmentService, IAttachmentRepository attachmentRepo, IMenuCategoryImagesRepository categoryImageRepo)
        {
            _unitOfWork = unitOfWork;
            _menuCategoryRepo = menuCategoryRepository;
            _attachmentService = attachmentService;
            _attachmentRepo = attachmentRepo;
            _categoryImageRepo = categoryImageRepo;
        }

        public void ActivateCategory(int id)
        {
            try
            {
                using (var tx = _unitOfWork.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    var entity = _menuCategoryRepo.GetById(id) ?? throw new CustomException("Menu category not found.");
                    entity.Status = Status.Active.ToString();
                    _menuCategoryRepo.Update(entity);
                    _unitOfWork.Complete();
                    tx.Commit();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int AddOrUpdate(MenuCategoryCreateDto model)
        {
            try
            {
                using (var tx = _unitOfWork.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    var entity = new MenuCategory();
                    if (model.Id > 0)
                    {
                        entity = _menuCategoryRepo.GetById(model.Id) ?? throw new CustomException("Menu category not found.");
                        entity.Name = model.Name;
                        entity.CompanyId = model.CompanyId;
                        entity.Description = model.Description;
                        _menuCategoryRepo.Update(entity);
                    }
                    else
                    {
                        entity.Name = model.Name;
                        entity.CompanyId = model.CompanyId;
                        entity.Description = model.Description;
                        entity.Status = Status.InActive.ToString();
                        _menuCategoryRepo.Insert(entity);
                    }

                    _unitOfWork.Complete();
                    foreach (var image in model.Images)
                    {
                        entity.Images.Add(new MenuCategoryImages()
                        {
                            AttachmentId = _attachmentService.CreateWihoutTransaction(image),
                            CategoryId = entity.Id,
                        });
                    }
                    _menuCategoryRepo.Update(entity);

                    _unitOfWork.Complete();
                    tx.Commit();
                    return entity.Id;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeActivateCategory(int id)
        {
            try
            {
                using (var tx = _unitOfWork.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    var entity = _menuCategoryRepo.GetById(id) ?? throw new CustomException("Menu category not found.");
                    entity.Status = Status.InActive.ToString();
                    _menuCategoryRepo.Update(entity);

                    _unitOfWork.Complete();
                    tx.Commit();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void RemoveImage(int categoryId, int attachmentId)
        {
            try
            {
                using (var tx = _unitOfWork.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    var entity = _menuCategoryRepo.GetById(categoryId) ?? throw new CustomException("Menu category not found.");
                    var attachment = _attachmentRepo.GetById(attachmentId) ?? throw new CustomException("Attachment not found.");
                    var menuCategoryImage = _categoryImageRepo.GetByAttachmentIdandCategoryId(categoryId, attachmentId) ?? throw new CustomException("Menu category image not found.");
                    entity.Images.Remove(menuCategoryImage);
                    _menuCategoryRepo.Update(entity);

                    _unitOfWork.Complete();
                    tx.Commit();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
