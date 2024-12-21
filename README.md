# KingdomHeartsMemoryInterface

WIP C# API for interfacing with Kingdom Hearts Final Mix's memory. Preconfigured for use with EPIC 1.0.0.10 version, but edit the offsets.csv file to match the offsets for your version and it will work.

# How to use

See TestApp for coding examples,  but you'll need to use both the KingdomHeartsMemoryInterface.dll and MemoryInterface.dll files in your project. MemoryInterface is a class used by all classes in KHMI to interface with the game's data, KHMI is more akin to an organizational layer to provide easy programmatic access to data.