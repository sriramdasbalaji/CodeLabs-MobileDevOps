using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyHealth.Client.Core.ViewModels;
using MyHealth.Client.Core.ServiceAgents;
using MvvmCross.Plugins.Messenger;
using System.Threading.Tasks;
using Moq;
using MyHealth.Client.Core.Model;
using System.Collections.Generic;

namespace MyHealth.Client.Core.UnitTests.ViewModels
{
    [TestClass]
    public class HomeViewModelTests
    {
        [TestMethod]
        public async Task Test_RetrieveAppointments_WhenZeroAppointments_InitsCorrectly()
        {
            #region arrange
            Mock<ClinicAppointmentsService> mockAppointmentService = GetMockAppointmentService(0);

            var mockHealthClient = new Mock<IMyHealthClient>();
            mockHealthClient.Setup(h => h.AppointmentsService).Returns(mockAppointmentService.Object);

            var mockMessenger = new Mock<IMvxMessenger>();
            #endregion

            // act
            var homeviewModel = new HomeViewModel(mockHealthClient.Object, mockMessenger.Object);
            await homeviewModel.RetrieveAppointmentsAsync();

            // assert
            Assert.IsNull(homeviewModel.FirstAppointment);
            Assert.IsNull(homeviewModel.SecondAppointment);
            Assert.IsNull(homeviewModel.Appointments);
        }

        [TestMethod]
        public async Task Test_RetrieveAppointments_WhenOneAppointment_InitsCorrectly()
        {
            #region arrange
            Mock<ClinicAppointmentsService> mockAppointmentService = GetMockAppointmentService(1);

            var mockHealthClient = new Mock<IMyHealthClient>();
            mockHealthClient.Setup(h => h.AppointmentsService).Returns(mockAppointmentService.Object);

            var mockMessenger = new Mock<IMvxMessenger>();
            #endregion

            // act
            var homeviewModel = new HomeViewModel(mockHealthClient.Object, mockMessenger.Object);
            await homeviewModel.RetrieveAppointmentsAsync();

            // assert
            Assert.IsNotNull(homeviewModel.FirstAppointment);
            Assert.AreEqual(1, homeviewModel.FirstAppointment.AppointmentId);
            Assert.IsNull(homeviewModel.SecondAppointment);
            Assert.IsNull(homeviewModel.Appointments);
        }

        [TestMethod]
        public async Task Test_RetrieveAppointments_WhenMoreThanOneAppointments_InitsCorrectly()
        {
            #region arrange
            Mock<ClinicAppointmentsService> mockAppointmentService = GetMockAppointmentService(3);

            var mockHealthClient = new Mock<IMyHealthClient>();
            mockHealthClient.Setup(h => h.AppointmentsService).Returns(mockAppointmentService.Object);

            var mockMessenger = new Mock<IMvxMessenger>();
            #endregion

            // act
            var homeviewModel = new HomeViewModel(mockHealthClient.Object, mockMessenger.Object);
            await homeviewModel.RetrieveAppointmentsAsync();

            // assert
            Assert.IsNotNull(homeviewModel.FirstAppointment);
            Assert.AreEqual(1, homeviewModel.FirstAppointment.AppointmentId);
            Assert.IsNotNull(homeviewModel.SecondAppointment);
            Assert.AreEqual(2, homeviewModel.SecondAppointment.AppointmentId);
            Assert.IsNotNull(homeviewModel.Appointments);
            Assert.AreEqual(1, homeviewModel.Appointments.Count);
            Assert.IsFalse(homeviewModel.Appointments.Any(a => a.AppointmentId == 1));
            Assert.IsFalse(homeviewModel.Appointments.Any(a => a.AppointmentId == 2));
            Assert.IsTrue(homeviewModel.Appointments.Any(a => a.AppointmentId == 3));
        }

        [TestMethod]
        public async Task Test_RetrieveMedicines_WhenZeroMeds_InitsCorrectly()
        {
            #region arrange
            Mock<MedicinesService> mockMedicineService = GetMockMedicineService(0);

            var mockHealthClient = new Mock<IMyHealthClient>();
            mockHealthClient.Setup(h => h.MedicinesService).Returns(mockMedicineService.Object);

            var mockMessenger = new Mock<IMvxMessenger>();
            #endregion

            // act
            var homeviewModel = new HomeViewModel(mockHealthClient.Object, mockMessenger.Object);
            await homeviewModel.RetrieveMedecinesAsync();

            // assert
            Assert.IsNull(homeviewModel.FirstMedicine);
            Assert.IsNull(homeviewModel.SecondMedicine);
            Assert.IsNull(homeviewModel.CurrentMedicine);
        }

        [TestMethod]
        public async Task Test_RetrieveMedicines_WhenOneMed_InitsCorrectly()
        {
            #region arrange
            Mock<MedicinesService> mockMedicineService = GetMockMedicineService(1);

            var mockHealthClient = new Mock<IMyHealthClient>();
            mockHealthClient.Setup(h => h.MedicinesService).Returns(mockMedicineService.Object);

            var mockMessenger = new Mock<IMvxMessenger>();
            #endregion

            // act
            var homeviewModel = new HomeViewModel(mockHealthClient.Object, mockMessenger.Object);
            await homeviewModel.RetrieveMedecinesAsync();

            // assert
            Assert.IsNotNull(homeviewModel.FirstMedicine);
            Assert.AreEqual(1, homeviewModel.FirstMedicine.Medicine.MedicineId);
            Assert.IsNull(homeviewModel.SecondMedicine);
            Assert.AreEqual(homeviewModel.CurrentMedicine, homeviewModel.FirstMedicine);
        }

        [TestMethod]
        public async Task Test_RetrieveMedicines_WhenTwoMeds_InitsCorrectly()
        {
            #region arrange
            Mock<MedicinesService> mockMedicineService = GetMockMedicineService(2);

            var mockHealthClient = new Mock<IMyHealthClient>();
            mockHealthClient.Setup(h => h.MedicinesService).Returns(mockMedicineService.Object);

            var mockMessenger = new Mock<IMvxMessenger>();
            #endregion

            // act
            var homeviewModel = new HomeViewModel(mockHealthClient.Object, mockMessenger.Object);
            await homeviewModel.RetrieveMedecinesAsync();

            // assert
            Assert.IsNotNull(homeviewModel.FirstMedicine);
            Assert.AreEqual(1, homeviewModel.FirstMedicine.Medicine.MedicineId);
            Assert.IsNotNull(homeviewModel.SecondMedicine);
            Assert.AreEqual(2, homeviewModel.SecondMedicine.Medicine.MedicineId);
            Assert.AreEqual(homeviewModel.CurrentMedicine, homeviewModel.FirstMedicine);
        }

        [TestMethod]
        public async Task Test_RetrieveMedicines_WhenThreeMeds_InitsCorrectly()
        {
            #region arrange
            Mock<MedicinesService> mockMedicineService = GetMockMedicineService(3);

            var mockHealthClient = new Mock<IMyHealthClient>();
            mockHealthClient.Setup(h => h.MedicinesService).Returns(mockMedicineService.Object);

            var mockMessenger = new Mock<IMvxMessenger>();
            #endregion

            // act
            var homeviewModel = new HomeViewModel(mockHealthClient.Object, mockMessenger.Object);
            await homeviewModel.RetrieveMedecinesAsync();

            // assert
            Assert.IsNotNull(homeviewModel.FirstMedicine);
            Assert.AreEqual(1, homeviewModel.FirstMedicine.Medicine.MedicineId);
            Assert.IsNotNull(homeviewModel.SecondMedicine);
            Assert.AreEqual(2, homeviewModel.SecondMedicine.Medicine.MedicineId);
            Assert.AreEqual(homeviewModel.CurrentMedicine, homeviewModel.FirstMedicine);
        }

        #region helper methods
        private static Mock<ClinicAppointmentsService> GetMockAppointmentService(int numAppointments)
        {
            var list = new List<ClinicAppointment>();
            for(int i = 0; i < numAppointments;)
            {
                list.Add(new ClinicAppointment() { AppointmentId = ++i });
            }
            var mockAppointmentService = new Mock<ClinicAppointmentsService>("url", 1);
            mockAppointmentService.Setup(a => a.GetPatientAppointmentsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(list);
            return mockAppointmentService;
        }

        private static Mock<MedicinesService> GetMockMedicineService(int numMeds)
        {
            var list = new List<MedicineWithDoses>();
            for (int i = 0; i < numMeds;)
            {
                list.Add(new MedicineWithDoses()
                    {
                        Medicine = new Medicine() { MedicineId = ++i },
                        Times = new Dictionary<TimeOfDay, int>() { { TimeOfDay.Breakfast, 1 } }
                    }
                );
            }
            var mockAppointmentService = new Mock<MedicinesService>("url", 1);
            mockAppointmentService.Setup(m => m.GetMedicinesWithDosesAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(list);
            return mockAppointmentService;
        }
        #endregion
    }
}
