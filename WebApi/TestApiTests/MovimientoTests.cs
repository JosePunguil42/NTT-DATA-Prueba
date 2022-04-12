using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DC;
using WebApi.Controllers;
using Microsoft.EntityFrameworkCore;
using WebApi.Logica;
using WebApi.Models;

namespace TestApi.Tests
{
    [TestClass()]
    public class MovimientoTests
    {
        private DbContextOptions<ApiContext> dbContextOptions = new DbContextOptionsBuilder<ApiContext>()
           .Options;

        [TestMethod()]
        public async Task ObtenerElSaldoDisponibleActualPorMovimiento()
        {
            //Arrange
            BIMovimientos bIMovimientos = new();
            const decimal saldoDisponible = 100;
            const decimal montoMovimiento = 200;
            const decimal resultado = 300;
            //Fac
            var saldo = await bIMovimientos.ObtenerSaldoDisponible(saldoDisponible, montoMovimiento);

            //Assert
            Assert.AreEqual(resultado, saldo);
        }

        [TestMethod()]
        public async Task ValidarQueElCupoDebitoEsteExecedido()
        {
            //Arrange
            BIMovimientos bIMovimientos = new();
            const decimal total = 400;
            const decimal montoMovimiento = 700;

            //Fac
            var cupo = await bIMovimientos.CupoDebitoExecedido(total, montoMovimiento);

            //Assert
            Assert.IsTrue(cupo);
        }

        [TestMethod()]
        public async Task ValidarQueElCupoDebitoNoEsteExecedido()
        {
            //Arrange
            BIMovimientos bIMovimientos = new();
            const decimal total = 200;
            const decimal montoMovimiento = 700;

            //Fac
            var cupo = await bIMovimientos.CupoDebitoExecedido(total, montoMovimiento);

            //Assert
            Assert.IsFalse(cupo);
        }

        [TestMethod()]
        public void AsignarDatosCuandoElObjetoSeaNull()
        {
            //Arrange
            BIMovimientos bIMovimientos = new();
            Movimiento movimiento = null;
            const decimal montoInicial = 300;
            //Fac
            bIMovimientos.AsignarDatosInicialesMovimientos(movimiento, montoInicial);

            //Assert
            Assert.IsNotNull(movimiento, "Objeto no null");
        }
    }
}