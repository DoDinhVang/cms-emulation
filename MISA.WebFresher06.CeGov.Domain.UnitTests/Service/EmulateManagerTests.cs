using AutoMapper;
using MISA.WebFresher06.CeGov.Domain;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher06.CeGov.Application.UnitTests
{
    public class EmulateManagerTests
    {
        private List<Emulate> _emulates;
        private List<EmulateDto> _emulatesDto;
        private IEmulateRepository _emulateRepository;
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            _emulateRepository = Substitute.For<IEmulateRepository>();
            _mapper = Substitute.For<IMapper>();
            _emulates = new List<Emulate>();
            _emulatesDto = new List<EmulateDto>();
            for (int i = 0; i < 10; ++i)
            {
                var emulate = new Emulate();
                var emulateDto = new EmulateDto();
                _emulates.Add(emulate);

                // Map dữ liệu emulate sang emulateDto
                _mapper.Map<EmulateDto>(emulate).Returns(emulateDto);
                _emulatesDto.Add(emulateDto);
            }
        }
        /// <summary>
        /// Hàm test EmualteCode chưa tồn tại
        /// </summary>
        /// <return></return>
        /// Created by: ddVang (25/08/2023)
        [Test]
        public async Task CheckDuplicateCode_EmulateCodeNotExist_Success()
        {
            //Arange
            string code = "Emulate-12";
            var emulateService = new EmulateService(_emulateRepository);
            //Act
            var result =  await emulateService.CheckDuplicateEmulateCodeAsync(code);
            //Assert
            Assert.IsFalse(result);
        }


        /// <summary>
        /// Hàm test EmualteCode đã tồn tại
        /// </summary>
        /// <return></return>
        /// Created by: ddVang (25/08/2023)
        [Test]
        public async Task CheckDuplicateCode_EmulateCodeExist_ReturnTrue()
        {
            //Arange
            string code = "string";
            var emulateService = new EmulateService(_emulateRepository);
            //Act
            var result =  await emulateService.CheckDuplicateEmulateCodeAsync(code);
            //Assert
            Assert.IsTrue(result);
        }
        /// <summary>
        /// Hàm test lấy tất cả bản ghi trong cơ sở dữ liệu
        /// </summary>
        /// <return></return>
        /// Created by: ddVang (25/08/2023)
        [Test]
        public async Task GetListAsync_NotInput_ReturnListEmulateDto()
        {
            // Arrange
            var expectedResult = _emulatesDto;
            //giả định gọi GetListAsync trên _emulateRepository và trả về danh sách _emulates
            var emulateService = new EmulateService(_emulateRepository);
            emulateService.GetListAsync().Returns(_emulatesDto);

            // Act
            var actualResult = await emulateService.GetListAsync();

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));

            // Kiểm tra Hàm GetListAsync có được gọi đúng 1 lần từ emulateRepository
            await _emulateRepository.Received(1).GetListAsync();
        }

        /// <summary>
        /// Hàm test lấy một bản ghi theo Id 
        /// </summary>
        /// <return></return>
        /// Created by: ddVang (25/08/2023)
        [Test]
        public async Task GetAsync_EmulationIDExist_Success()
        {
            // Arrange
            var id = Guid.NewGuid();
            var emulateService = new EmulateService(_emulateRepository);

            // Act
            var actualResult = await emulateService.GetAsync(id);

            // Assert
            Assert.That(actualResult.EmulateId, Is.EqualTo(id));

            // Kiểm tra Hàm GetAsync có được gọi đúng 1 lần từ emulateRepository
            await _emulateRepository.Received(1).GetAsync(id);
        }

        /// <summary>
        /// Hàm test xóa bản ghi đã có trong hệ thống
        /// </summary>
        /// <return></return>
        /// Created by: ddVang (25/08/2023)
        [Test]
        public async Task DeleteAsync_RecordExists_Success()
        {
            // Arrange
            var id = Guid.NewGuid();
            //expectedResult là 1 vì sử dụng phương thức ExecuteAsync. Nó sẽ tả về số lượng bảng ghi thay đổi
            int expectedResult = 1;
            var emulate = new Emulate();

            _emulateRepository.GetAsync(id).Returns(emulate);
            _emulateRepository.DeleteAsync(emulate.EmulateId).Returns(1);
            var emulateService = new EmulateService(_emulateRepository);

            // Act
            var actualResult = await emulateService.DeleteAsync(id);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));

            // Kiểm tra Hàm GetAsync có được gọi đúng 1 lần từ emulateRepository
            await _emulateRepository.Received(1).GetAsync(id);
            // Kiểm tra Hàm DeleteAsync có được gọi đúng 1 lần từ emulateRepository
            await _emulateRepository.Received(1).DeleteAsync(emulate.EmulateId);
        }

        /// <summary>
        /// Hàm test xóa nhiều bản ghi vói các id truyền vào là hợp lệ
        /// </summary>
        /// <return></return>
        /// Created by: ddVang (25/08/2023)
        [Test]
        public async Task DeleteManyAsync_IdsIsExist_Success()
        {
            // Arrange
            int expectedResult = 3;
            var ids = new List<Guid>();
            var emulates = new List<Emulate>();

            for (int i = 0; i < 3; i++)
            {
                var id = Guid.NewGuid();
                var emulation = new Emulate() {
                    EmulateId = id 
                };

                ids.Add(id);
            }

            var emulateService = new EmulateService(_emulateRepository);

            // Act
            var actualResult = await emulateService.DeleteManyAsync(ids);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));

            await _emulateRepository.Received(1).DeleteManyAsync(ids);
        }

        /// <summary>
        /// Hàm test thêm mới một bản ghi
        /// </summary>
        /// <return></return>
        /// Created by: ddVang (25/08/2023)
        [Test]
        public async Task AddAsync_EmualateCreateDto_Success()
        {
            // Arrage
            var emulateCreateDto = new EmulateCreateDto();
            var emulate = new Emulate();
            _mapper.Map<Emulate>(emulateCreateDto).Returns(emulate);
            int expectedResult = 1;
            var emulateService = new EmulateService(_emulateRepository);

            // Act
            var actualResult = await emulateService.AddAsync(emulateCreateDto);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));

            await _emulateRepository.Received(1).AddAsync(emulate);
        }

        /// <summary>
        /// Hàm test cập nhật bản ghi
        /// </summary>
          /// <return></return>
        /// Created by: ddVang (25/08/2023)
        [Test]
        public async Task UpdateAsync_EntityUpdateDto_Success()
        {
            // Arrange 
            var id = Guid.NewGuid();
            int expectedResult = 1;
            var emulateUpdateDto = new EmulateUpdateDto();
            var emulate = new Emulate();
            _mapper.Map<Emulate>(emulateUpdateDto).Returns(emulate);
            _emulateRepository.GetAsync(id).Returns(emulate);
            var emulateService = new EmulateService(_emulateRepository);

            // Act
            var actualResult = await emulateService.UpdateAsync(id, emulateUpdateDto);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));

            await _emulateRepository.Received(1).UpdateAsync(id,emulate);
        }
    }
}
