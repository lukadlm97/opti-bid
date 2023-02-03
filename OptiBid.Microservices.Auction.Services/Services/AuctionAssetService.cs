using AutoMapper;
using OptiBid.Microservices.Auction.Domain.Entities;
using OptiBid.Microservices.Auction.Services.Enumerations;
using OptiBid.Microservices.Auction.Services.Models;
using OptiBid.Microservices.Auction.Services.UnitOfWork;
using OptiBid.Microservices.Auction.Services.Utilities;
using OptiBid.Microservices.Shared.Messaging.DTOs;
using OptiBid.Microservices.Shared.Messaging.Enumerations;

namespace OptiBid.Microservices.Auction.Services.Services
{
    public class AuctionAssetService:IAuctionAssetService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFireForgetHandler _fireForgetHandler
            ;

        public AuctionAssetService(IUnitOfWork unitOfWork,IMapper mapper,IFireForgetHandler fireForgetHandler) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fireForgetHandler = fireForgetHandler;
        }
        public async Task<AuctionAssetsResponse> GetAll(CancellationToken cancellationToken = default)
        {
            try
            {
                var auctionAssets = _unitOfWork._auctionAssetsRepository.GetAll();

                if (auctionAssets == null || auctionAssets?.Count() == 0)
                {
                    return new AuctionAssetsResponse()
                    {
                        SearchStatus = SearchStatus.NotFound,
                    };
                }
                var mappedAssets = new List<Domain.DTOs.AuctionAsset>();
                foreach (var auctionAsset in auctionAssets)
                {
                    mappedAssets.Add(_mapper.Map<Domain.DTOs.AuctionAsset>(auctionAsset));
                }
                
                return new AuctionAssetsResponse()
                {
                    SearchStatus = SearchStatus.Success,
                    Assets = mappedAssets
                };
            }
            catch (Exception ex)
            {
                return new AuctionAssetsResponse()
                {
                    SearchStatus = SearchStatus.BadRequest,
                };
            }
         
        }

        public async Task<AuctionAssetsResponse> GetById(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var singleAsset = await _unitOfWork._auctionAssetsRepository.GetById(id, cancellationToken);
                if (singleAsset == null)
                {
                    return new AuctionAssetsResponse()
                    {
                        SearchStatus = SearchStatus.NotFound
                    };
                }

                return new AuctionAssetsResponse()
                {
                    SearchStatus = SearchStatus.Success,
                    Asset = _mapper.Map<Domain.DTOs.AuctionAsset>(singleAsset)
                };
            }
            catch (Exception ex)
            {
                return new AuctionAssetsResponse()
                {
                    SearchStatus = SearchStatus.BadRequest
                };
            }
           
        }

        public async Task<AuctionAssetsResponse> Create(Domain.Input.AuctionAsset auctionAsset, CancellationToken cancellationToken = default)
        {
            try
            {
                if (auctionAsset.ProductTypeId == null && auctionAsset.ServiceTypeId == null)
                {
                    return new AuctionAssetsResponse()
                    {
                        CreationStatus = CreationStatus.BadRequest
                    };
                }
                var customer =
                    await _unitOfWork._customerRepository.FindById(auctionAsset.CustomerId, cancellationToken);
                if (auctionAsset.ProductTypeId != null)
                {
                    var product = _mapper.Map<Domain.Entities.Product>(auctionAsset);
                    
                    var productCategory = 
                        _unitOfWork._productCategoryRepository.GetAll().FirstOrDefault(x=>x.Id==auctionAsset.ProductTypeId);

                    if (productCategory == null || customer == null)
                    {
                        return new AuctionAssetsResponse()
                        {
                            CreationStatus = CreationStatus.BadRequest
                        };
                    }
                    product.ProductCategory=productCategory;
                    product.ProductCategoryID=productCategory.Id;

                    product.Customer = customer;
                    product.CustomerID = customer.Id;
                    product.Bids = new List<Bid>();
                    product.MediaUrls=new List<MediaUrl>();

                    await _unitOfWork._auctionAssetsRepository.Add(product);
                    await _unitOfWork.Commit(cancellationToken);

                    _fireForgetHandler.Execute(x => x.Send(new AuctionMessage()
                    {
                        ActionType = AuctionMessageType.Added,
                        AssetType = AssetMessageType.Product,
                        Description = product.Description,
                        ID = product.Id,
                        Username = customer.Username,
                        Name = product.Name
                    }));
                    return new AuctionAssetsResponse()
                    {
                        CreationStatus = CreationStatus.Success,
                        Asset = _mapper.Map<Domain.DTOs.AuctionAsset>(product)
                    };
                }


                var service = _mapper.Map<Domain.Entities.Service>(auctionAsset);
                var serviceCategory =
                    _unitOfWork._serviceCategoryRepository.GetAll().FirstOrDefault(x => x.Id == auctionAsset.ServiceTypeId);

                if (serviceCategory == null || customer == null)
                {
                    return new AuctionAssetsResponse()
                    {
                        CreationStatus = CreationStatus.BadRequest
                    };
                }
                service.ServiceCategory = serviceCategory;
                service.ServiceCategoryID = serviceCategory.Id;

                service.Customer = customer;
                service.CustomerID = customer.Id;
                service.Bids = new List<Bid>();
                service.MediaUrls = new List<MediaUrl>();
                
                await _unitOfWork._auctionAssetsRepository.Add(service);
                await _unitOfWork.Commit(cancellationToken);


                _fireForgetHandler.Execute(x => x.Send(new AuctionMessage()
                {
                     ActionType = AuctionMessageType.Added,
                    AssetType = AssetMessageType.Service,
                    Description = service.Description,
                    ID = service.Id,
                    Username = customer.Username,
                    Name = service.Name
                }));
                return new AuctionAssetsResponse()
                {
                    CreationStatus = CreationStatus.Success,
                    Asset = _mapper.Map<Domain.DTOs.AuctionAsset>(service)
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new AuctionAssetsResponse()
                {
                    CreationStatus = CreationStatus.Error
                };
            }
           

        }

        public async Task<AuctionAssetsResponse> Update(Domain.Input.AuctionAsset auctionAsset, CancellationToken cancellationToken = default)
        {
            try
            {
                if (auctionAsset.ProductTypeId == null || auctionAsset.ServiceTypeId == null)
                {
                    return new AuctionAssetsResponse()
                    {
                        CreationStatus = CreationStatus.BadRequest
                    };
                }

                if (auctionAsset.ProductTypeId != null)
                {
                    var product = _mapper.Map<Domain.Entities.Product>(auctionAsset);
                    
                  
                    await _unitOfWork._auctionAssetsRepository.Update(product);
                    await _unitOfWork.Commit(cancellationToken);
                    _fireForgetHandler.Execute(x => x.Send(new AuctionMessage()
                    {
                        ActionType = AuctionMessageType.Added,
                        AssetType = AssetMessageType.Service,
                        Description = product.Description,
                        ID = product.Id,
                        Username = "",
                        Name = product.Name
                    }));

                    return new AuctionAssetsResponse()
                    {
                        CreationStatus = CreationStatus.Success,
                        Asset = _mapper.Map<Domain.DTOs.AuctionAsset>(product)
                    };
                }


                var service = _mapper.Map<Domain.Entities.Product>(auctionAsset); 
              
                await _unitOfWork._auctionAssetsRepository.Update(service);
                await _unitOfWork.Commit(cancellationToken);
                _fireForgetHandler.Execute(x => x.Send(new AuctionMessage()
                {
                    ActionType = AuctionMessageType.Added,
                    AssetType = AssetMessageType.Service,
                    Description = service.Description,
                    ID = service.Id,
                    Username = "",
                    Name = service.Name
                }));

                return new AuctionAssetsResponse()
                {
                    CreationStatus = CreationStatus.Success,
                    Asset = _mapper.Map<Domain.DTOs.AuctionAsset>(service)
                };
            }
            catch (Exception ex)
            {
                return new AuctionAssetsResponse()
                {
                    CreationStatus = CreationStatus.BadRequest
                };
            }
           
        }

        public async Task<AuctionAssetsResponse> Delete(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var auctionAsset = await _unitOfWork._auctionAssetsRepository.GetById(id, cancellationToken);
                if (auctionAsset == null)
                {
                    return new AuctionAssetsResponse()
                    {
                        CreationStatus = CreationStatus.Error
                    };
                }

                await _unitOfWork._auctionAssetsRepository.Delete(auctionAsset, cancellationToken);
                await _unitOfWork.Commit(cancellationToken);
                _fireForgetHandler.Execute(x => x.Send(new AuctionMessage()
                {
                    ActionType = AuctionMessageType.Deleted,
                    AssetType =null,
                    ID = id
                }));
                return new AuctionAssetsResponse()
                {
                    CreationStatus = CreationStatus.Success
                };
            }
            catch (Exception ex)
            {
                return new AuctionAssetsResponse()
                {
                    CreationStatus = CreationStatus.Error
                };
            }
           

        }
    }
}
