using Modbus.FunctionParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modbus.ModbusFunctions
{
	public static class PackRequestHelper
	{
		public static byte[] WriteModbusFunctionPackRequest(ModbusCommandParameters commandParameters)
		{
			byte[] setToReturn = new byte[12];

			setToReturn[0] = BitConverter.GetBytes(commandParameters.TransactionId)[1];
			setToReturn[1] = BitConverter.GetBytes(commandParameters.TransactionId)[0];
			setToReturn[2] = BitConverter.GetBytes(commandParameters.ProtocolId)[1];
			setToReturn[3] = BitConverter.GetBytes(commandParameters.ProtocolId)[0];
			setToReturn[4] = BitConverter.GetBytes(commandParameters.Length)[1];
			setToReturn[5] = BitConverter.GetBytes(commandParameters.Length)[0];
			setToReturn[6] = commandParameters.UnitId;
			setToReturn[7] = commandParameters.FunctionCode;

			ModbusWriteCommandParameters modbusWriteCommandParameters = (ModbusWriteCommandParameters)commandParameters;

			setToReturn[8] = BitConverter.GetBytes(modbusWriteCommandParameters.OutputAddress)[1];
			setToReturn[9] = BitConverter.GetBytes(modbusWriteCommandParameters.OutputAddress)[0];
			setToReturn[10] = BitConverter.GetBytes(modbusWriteCommandParameters.Value)[1];
			setToReturn[11] = BitConverter.GetBytes(modbusWriteCommandParameters.Value)[0];

			return setToReturn;
		}

		public static byte[] ReadModbusFunctionPackRequest(ModbusCommandParameters commandParameters)
		{
			byte[] setToReturn = new byte[12];

			setToReturn[0] = BitConverter.GetBytes(commandParameters.TransactionId)[1];
			setToReturn[1] = BitConverter.GetBytes(commandParameters.TransactionId)[0];
			setToReturn[2] = BitConverter.GetBytes(commandParameters.ProtocolId)[1];
			setToReturn[3] = BitConverter.GetBytes(commandParameters.ProtocolId)[0];
			setToReturn[4] = BitConverter.GetBytes(commandParameters.Length)[1];
			setToReturn[5] = BitConverter.GetBytes(commandParameters.Length)[0];
			setToReturn[6] = commandParameters.UnitId;
			setToReturn[7] = commandParameters.FunctionCode;

			ModbusReadCommandParameters modbusReadCommandParameters = (ModbusReadCommandParameters)commandParameters;

			setToReturn[8] = BitConverter.GetBytes(modbusReadCommandParameters.StartAddress)[1];
			setToReturn[9] = BitConverter.GetBytes(modbusReadCommandParameters.StartAddress)[0];
			setToReturn[10] = BitConverter.GetBytes(modbusReadCommandParameters.Quantity)[1];
			setToReturn[11] = BitConverter.GetBytes(modbusReadCommandParameters.Quantity)[0];

			return setToReturn;
		}
	}
}
