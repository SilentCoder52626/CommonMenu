using DomainModule.Dto.Menu;
using DomainModule.Entity.Menu;
using DomainModule.Enums;
using DomainModule.Exceptions;
using DomainModule.RepositoryInterface;
using DomainModule.RepositoryInterface.Menu;
using DomainModule.ServiceInterface;
using DomainModule.ServiceInterface.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModule.Service.Menu
{
    public class ItemService : IItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IItemRepository _itemRepo;
        private readonly IAttachmentService _attachmentService;

        public ItemService(IUnitOfWork unitOfWork, IItemRepository itemRepo, IAttachmentService attachmentService)
        {
            _unitOfWork = unitOfWork;
            _itemRepo = itemRepo;
            _attachmentService = attachmentService;
        }


        public void ActivateStatus(int id)
        {
            try
            {
                using (var tx = _unitOfWork.BeginTransaction())
                {
                    var Item = _itemRepo.GetById(id) ?? throw new CustomException("Item not found.");

                    Item.Status = Status.Active.ToString();

                    _unitOfWork.Complete();
                    tx.Commit();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int AddOrUpdateItem(ItemCreateDto model)
        {

            try
            {
                using (var tx = _unitOfWork.BeginTransaction())
                {
                    var entity = new Item();
                    if (model.Id > 0)
                    {
                        entity = _itemRepo.GetById(model.Id) ?? throw new CustomException("Item not found.");
                        ConfigureItemEntity(model, entity);
                        _itemRepo.Update(entity);
                    }
                    else
                    {
                        ConfigureItemEntity(model, entity);
                        entity.Status = Status.Active.ToString();
                        _itemRepo.Insert(entity);
                    }
                    _unitOfWork.Complete();
                    if(model.Image != null && !String.IsNullOrEmpty(model.Image.FileName))
                    {
                        var currentAttachmentId = entity.ImageId.GetValueOrDefault();
                        entity.ImageId = _attachmentService.CreateWihoutTransaction(model.Image);
                        if (currentAttachmentId > 0)
                            _attachmentService.DeleteWithoutTransasction(currentAttachmentId);
                    }
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

        private static void ConfigureItemEntity(ItemCreateDto model, Item entity)
        {
            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.CategoryId = model.CategoryId;
            entity.Price = model.Price;
        }

        public void DeactivateStatus(int id)
        {
            try
            {
                using (var tx = _unitOfWork.BeginTransaction())
                {
                    var Item = _itemRepo.GetById(id) ?? throw new CustomException("Item not found.");

                    Item.Status = Status.InActive.ToString();

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
