/*
	DataFlash.cpp - DataFlash log library for AT45DB161
	Code by Jordi Muñoz and Jose Julio. DIYDrones.com
	This code works with boards based on ATMega168/328 and ATMega1280/2560 using SPI port

	This library is free software; you can redistribute it and/or
    modify it under the terms of the GNU Lesser General Public
    License as published by the Free Software Foundation; either
    version 2.1 of the License, or (at your option) any later version.

	Dataflash library for AT45DB161D flash memory
	Memory organization : 4096 pages of 512 bytes or 528 bytes

	Maximun write bandwidth : 512 bytes in 14ms
	This code is written so the master never has to wait to write the data on the eeprom

	Methods:
		Init() : Library initialization (SPI initialization)
		StartWrite(page) : Start a write session. page=start page.
		WriteByte(data) : Write a byte
		WriteInt(data) :  Write an integer (2 bytes)
		WriteLong(data) : Write a long (4 bytes)
		StartRead(page) : Start a read on (page)
		GetWritePage() : Returns the last page written to
		GetPage() : Returns the last page read
		ReadByte()
		ReadInt()
		ReadLong()
		
	Properties:
			
*/

extern "C" {
  // AVR LibC Includes
  #include <inttypes.h>
  #include <avr/interrupt.h>
  #include "WConstants.h"
}

#include "DataFlash.h"

#define OVERWRITE_DATA 0 // 0: When reach the end page stop, 1: Start overwritten from page 1

// *** INTERNAL FUNCTIONS ***
unsigned char dataflash_SPI_transfer(unsigned char output)
{
  return 0; 
}

void dataflash_CS_inactive()
{
}

void dataflash_CS_active()
{
}

// Constructors ////////////////////////////////////////////////////////////////
DataFlash_Class::DataFlash_Class()
{
}

// Public Methods //////////////////////////////////////////////////////////////
void DataFlash_Class::Init(void)
{
}

// This function is mainly to test the device
void DataFlash_Class::ReadManufacturerID()
{
}

// Read the status register
byte DataFlash_Class::ReadStatusReg()
{ 
  return 0; 
}

// Read the status of the DataFlash
inline
byte DataFlash_Class::ReadStatus()
{ 
  return 0; 
}


inline
unsigned int DataFlash_Class::PageSize()
{ 
  return 0; 
}


// Wait until DataFlash is in ready state...
void DataFlash_Class::WaitReady()
{
}

void DataFlash_Class::PageToBuffer(unsigned char BufferNum, unsigned int PageAdr)
{
}

void DataFlash_Class::BufferToPage (unsigned char BufferNum, unsigned int PageAdr, unsigned char wait)
{
}

void DataFlash_Class::BufferWrite (unsigned char BufferNum, unsigned int IntPageAdr, unsigned char Data)
{
}
  
unsigned char DataFlash_Class::BufferRead (unsigned char BufferNum, unsigned int IntPageAdr)
{
  return 0; 
}
// *** END OF INTERNAL FUNCTIONS ***

void DataFlash_Class::PageErase (unsigned int PageAdr)
{
}


void DataFlash_Class::ChipErase ()
{
}

// *** DATAFLASH PUBLIC FUNCTIONS ***
void DataFlash_Class::StartWrite(int PageAdr)
{
}

void DataFlash_Class::FinishWrite(void)
{
}
	

void DataFlash_Class::WriteByte(byte data)
{
}

void DataFlash_Class::WriteInt(int data)
{
}

void DataFlash_Class::WriteLong(long data)
{
}

// Get the last page written to
int DataFlash_Class::GetWritePage() 
{
  return 0; 
}

// Get the last page read
int DataFlash_Class::GetPage() 
{
  return 0; 
}

void DataFlash_Class::StartRead(int PageAdr)
{
}

byte DataFlash_Class::ReadByte()
{
  return 0; 
}

int DataFlash_Class::ReadInt()
{
  return 0; 
}

long DataFlash_Class::ReadLong()
{
  return 0; 
}

// make one instance for the user to use
DataFlash_Class DataFlash;