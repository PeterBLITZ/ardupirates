###############################################################################
# Makefile for the project ArduPirates
###############################################################################

## General Flags
PROJECT = ArduPirates
MCU = atmega128
TARGET = ArduPirates.elf
CC = avr-gcc

CPP = avr-g++

## Options common to compile, link and assembly rules
COMMON = -mmcu=$(MCU)

## Compile options common for all C compilation units.
CFLAGS = $(COMMON)
CFLAGS += -Wall -gdwarf-2 -Os -funsigned-char -funsigned-bitfields -fpack-struct  -fshort-enums  LDFLAGS = /Llibaries 
CFLAGS += -MD -MP -MT $(*F).o -MF dep/$(@F).d 

## Assembly specific flags
ASMFLAGS = $(COMMON)
ASMFLAGS += $(CFLAGS)
ASMFLAGS += -x assembler-with-cpp -Wa,-gdwarf2

## Linker flags
LDFLAGS = $(COMMON)
LDFLAGS +=  -Wl,-Map=ArduPirates.map


## Intel Hex file production flags
HEX_FLASH_FLAGS = -R .eeprom -R .fuse -R .lock -R .signature

HEX_EEPROM_FLAGS = -j .eeprom
HEX_EEPROM_FLAGS += --set-section-flags=.eeprom="alloc,load"
HEX_EEPROM_FLAGS += --change-section-lma .eeprom=0 --no-change-warnings


## Library Directories
LIBDIRS = -L"D:\Arduino\ardupirates\QUAD\ArduCopter_SuperStable\libraries" 

## Objects that must be built in order to link
OBJECTS = ArduCopter_SuperStable.o DCM.o Functions.o Log.o Navigation.o Sensors.o SerialCom.o 

## Objects explicitly added by the user
LINKONLYOBJECTS = 

## Build
all: $(TARGET) ArduPirates.hex ArduPirates.eep ArduPirates.lss size

## Compile
ArduCopter_SuperStable.o: ../ArduCopter_SuperStable.cpp
	$(CPP) $(INCLUDES) $(CFLAGS) -c  $<

DCM.o: ../DCM.cpp
	$(CPP) $(INCLUDES) $(CFLAGS) -c  $<

Functions.o: ../Functions.cpp
	$(CPP) $(INCLUDES) $(CFLAGS) -c  $<

Log.o: ../Log.cpp
	$(CPP) $(INCLUDES) $(CFLAGS) -c  $<

Navigation.o: ../Navigation.cpp
	$(CPP) $(INCLUDES) $(CFLAGS) -c  $<

Sensors.o: ../Sensors.cpp
	$(CPP) $(INCLUDES) $(CFLAGS) -c  $<

SerialCom.o: ../SerialCom.cpp
	$(CPP) $(INCLUDES) $(CFLAGS) -c  $<

##Link
$(TARGET): $(OBJECTS)
	 $(CPP) $(LDFLAGS) $(OBJECTS) $(LINKONLYOBJECTS) $(LIBDIRS) $(LIBS) -o $(TARGET)

%.hex: $(TARGET)
	avr-objcopy -O ihex $(HEX_FLASH_FLAGS)  $< $@

%.eep: $(TARGET)
	-avr-objcopy $(HEX_EEPROM_FLAGS) -O ihex $< $@ || exit 0

%.lss: $(TARGET)
	avr-objdump -h -S $< > $@

size: ${TARGET}
	@echo
	@avr-size -C --mcu=${MCU} ${TARGET}

## Clean target
.PHONY: clean
clean:
	-rm -rf $(OBJECTS) ArduPirates.elf dep/* ArduPirates.hex ArduPirates.eep ArduPirates.lss ArduPirates.map


## Other dependencies
-include $(shell mkdir dep 2>NUL) $(wildcard dep/*)

