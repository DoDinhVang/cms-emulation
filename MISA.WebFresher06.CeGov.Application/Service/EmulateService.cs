using MISA.WebFresher06.CeGov.Application;
using MISA.WebFresher06.CeGov.Application.Dto;
using MISA.WebFresher06.CeGov.Domain;
using MISA.WebFresher06.CeGov.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher06.CeGov.Application
{
    public class EmulateService : BaseCrudService<Emulate, EmulateDto, EmulateUpdateDto, EmulateCreateDto, Guid>, IEmulateService
    {
        private readonly IEmulateRepository _emulateRepository;
        public EmulateService(IEmulateRepository repository, IRewarderRepository rewarderRepository) : base(repository)
        {
            _emulateRepository = repository;
        }

        public async Task<bool> CheckDuplicateEmulateCodeAsync(string code)
        {
            var result = await _emulateRepository.FindByEmulateCodeAsync(code);
            if (result == null)
            {
                return false;
            }
            return true;
        }
        public async Task<string> getEmulateCode()
        {
            const string characters = "0123456789";
            Random random = new Random();
            int minLength = 4;
            int maxLength = 7;
            int length = random.Next(minLength, maxLength + 1);
            string emulateCode;
            var isExistedEmulateCode = false;
            do
            {
                emulateCode = new string(Enumerable.Repeat(characters, length)
                  .Select(s => s[random.Next(s.Length)]).ToArray());
                isExistedEmulateCode = await CheckDuplicateEmulateCodeAsync(emulateCode);
            } while (isExistedEmulateCode);
            return $"EM-{emulateCode}";
        }

        public async Task<List<EmulateDetailDto>> GetListEmulateDetail()
        {
            var data = await _emulateRepository.GetListEmulate();
            var result = data.Select(emuDetail => MapEmulateDetailToEmulateDetailDto(emuDetail));
            return result.ToList();
        }
        public async Task<(IEnumerable<EmulateDetailDto>, int, int, int)> GetPageEmulateAsync(PagingInfoDto pagingInfo)
        {
            var (entities, pageIndex, pageSize, totalItems) = await _emulateRepository.GetPageEmulateAsync(pagingInfo.PageIndex, pagingInfo.PageSize, pagingInfo.KeyWord, pagingInfo.RewardObj, pagingInfo.RewardType, pagingInfo.RewarderId, pagingInfo.Status);

            var result = entities.Select(entity => MapEmulateDetailToEmulateDetailDto(entity)).ToList();
            return (result, pageIndex, pageSize, totalItems);
        }
        public async Task<int> ChangeStatus(Guid id, EmulateStatus status)
        {
            var emulate = await GetAsync(id);
            if(emulate ==  null)
            {
                throw new BadRequestException($"{ErrorMessage.NonExistingIds}:{id}");
            }
            var result = await _emulateRepository.ChangeStatus(id, status);
            return result;
        }
        public async Task<int> ChangeMultipleStatus(List<Guid> ids, EmulateStatus status)
        {
            var existingIds = await GetExistingIdsAsync(ids);
            var nonExistingIds = ids.Except(existingIds).ToList();
            if (nonExistingIds.Count != 0)
            {
                throw new BadRequestException($"{ErrorMessage.NonExistingIds}: {string.Join(',', nonExistingIds)}");
            }
            var result = await _emulateRepository.ChangeMultipleStatus(ids, status);
            return result;
        }

        public async override Task<Emulate> MapTEntityCreateDtoToTEntity(EmulateCreateDto emulateCreateDto)
        {
            var existingCode = await CheckDuplicateEmulateCodeAsync(emulateCreateDto.EmulateCode);
            if (existingCode)
            {
                throw new ConflictException($"Mã danh hiệu ${emulateCreateDto.EmulateCode} đã tồn tại. Vui lòng nhập mã khác.");
            }
            var emulate = new Emulate();
            emulate.EmulateId = Guid.NewGuid();
            emulate.CreatedDate = DateTime.Now;
            emulate.EmulateCode = emulateCreateDto.EmulateCode;
            emulate.EmulateName = emulateCreateDto.EmulateName;
            emulate.RewardObj = emulateCreateDto.RewardObj;
            emulate.RewardType = emulateCreateDto.RewardType;
            emulate.RewarderId = emulateCreateDto.RewarderId;
            emulate.Description = emulateCreateDto.Description;
            return emulate;
        }

        public override EmulateDto MapTEntityToEntityDto(Emulate emulate)
        {
            var emulateDto = new EmulateDto();
            emulateDto.EmulateName = emulate.EmulateName;
            emulateDto.RewardObj = emulate.RewardObj;
            emulateDto.RewardType = emulate.RewardType;
            emulateDto.RewarderId = emulate.RewarderId;
            emulateDto.Description = emulate.Description;
            emulateDto.EmulateId = emulate.EmulateId;
            emulateDto.EmulateCode = emulate.EmulateCode;
            emulateDto.Status = emulate.Status;
            emulateDto.CreatedDate = emulate.CreatedDate;
            emulateDto.CreatedBy = emulate.CreatedBy;
            emulateDto.ModifiedDate = emulate.ModifiedDate;
            emulateDto.ModifiedBy = emulate.ModifiedBy;
            return emulateDto;
        }
        public async override Task<Emulate> MapTEntityUpdateDtoToTEntity(Guid id,EmulateUpdateDto emulateUpdateDto)
        {
            var emulateCurrent =  await GetAsync(id);
            if (emulateCurrent == null)
            {
                throw new BadRequestException($"{id} không đúng.");
            }
            else
            {
                var emulateCodeCurrent = emulateCurrent.EmulateCode;
                if (emulateCodeCurrent != emulateUpdateDto.EmulateCode)
                {
                    var existingCode = await CheckDuplicateEmulateCodeAsync(emulateUpdateDto.EmulateCode);
                    if (existingCode)
                    {
                        throw new ConflictException($"Mã danh hiệu ${emulateUpdateDto.EmulateCode} đã tồn tại. Vui lòng nhập mã khác.");
                    }
                }
                var emulate = new Emulate();
                emulate.EmulateCode = emulateUpdateDto.EmulateCode;
                emulate.EmulateName = emulateUpdateDto.EmulateName;
                emulate.RewardObj = emulateUpdateDto.RewardObj;
                emulate.RewardType = emulateUpdateDto.RewardType;
                emulate.RewarderId = emulateUpdateDto.RewarderId;
                emulate.Description = emulateUpdateDto.Description;
                emulate.ModifiedDate = DateTime.Now;
                return emulate;

            }
        }
        public EmulateDetailDto MapEmulateDetailToEmulateDetailDto(EmulateDetail emu)
        {
            var emulateDetailDto = new EmulateDetailDto();
            emulateDetailDto.EmulateName = emu.EmulateName;
            emulateDetailDto.RewardObj = emu.RewardObj;
            emulateDetailDto.RewardType = emu.RewardType;
            emulateDetailDto.RewarderId = emu.RewarderId;
            emulateDetailDto.RewarderName = emu.RewarderName;
            emulateDetailDto.Description = emu.Description;
            emulateDetailDto.EmulateId = emu.EmulateId;
            emulateDetailDto.EmulateCode = emu.EmulateCode;
            emulateDetailDto.Status = emu.Status;
            emulateDetailDto.CreatedDate = emu.CreatedDate;
            emulateDetailDto.CreatedBy = emu.CreatedBy;
            emulateDetailDto.ModifiedDate = emu.ModifiedDate;
            emulateDetailDto.ModifiedBy = emu.ModifiedBy;
            return emulateDetailDto;
        }

        public async Task<UploadExelFileResponse> UploadExcelFile(UploadExelFileRequest request, string path)
        {
            var result = await _emulateRepository.UploadExcelFile(request, path);
            return result;
        }
    }
}
        