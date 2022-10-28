SRC1 = Main.cs
EXE = $(SRC1:.cs=.exe)

CSC = mcs

all : $(EXE)

# -lm links the math library
$(EXE): $(SRC1)
	$(CSC) $(SRC1) -out:$(EXE)

# Removes all object files, denoted with the extension .o
clean:
	rm -rf *.o *~

# Removes all object and the executable files, denoted with the extensions .o and .exe
cleanall:
	rm -rf *o $(EXE) *~

# Builds the program and runs it
buildandrun : $(EXE)
	./$(EXE)

