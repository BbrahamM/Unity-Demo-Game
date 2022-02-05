// AridmeticProblem.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <stdlib.h> 
#include <fstream>

int menu_selection = 0;
int num_to_find = 0;

int num_to_find_2 = 0;



void Numbers();

int main()
{

	while (true)
	{
		std::cout << "\tMenu\n";
		std::cout << "1. Ingresar Numeros\n";
		std::cout << "2. Salir\n";
		std::cin >> menu_selection;
		system("CLS");


		if (menu_selection == 1) {
			Numbers();
		}
		else
		{
			std::cerr << "Adios ";
			return 0;
		}

	}
}


void Numbers() {
	std::cout << "\tMenu Numeros\n";
	std::cout << "Ingresa Cuantos Numeros Deseas Ingresar: ";
	std::cin >> num_to_find;
	int * numToFind = new int [num_to_find];
	std::cout << "\n";
	for (int i = 0; i < num_to_find; i++)
	{
		std::cout << (i+1) << " Ingresa Numero : ";
		int num = 0;
		std::cin >> num;
		numToFind[i] = num;
	}
	std::cout << "\n Los Numeros que Ingresaste Fueron\n\t";
	
	for (int i = 0; i < num_to_find; i++) {
		std::cout << " " << numToFind[i];
	}

	std::cout << "\nIngresa Cuantos Numeros Deseas Buscar: ";
	std::cin >> num_to_find_2;

	int* numToFind_2 = new int[num_to_find_2];

	std::cout << "\n";
	for (int i = 0; i < num_to_find_2; i++)
	{
		std::cout << (i + 1) << " Ingresa Numero a Buscar: ";
		int num = 0;
		std::cin >> num;
		numToFind_2[i] = num;
	}

	std::cout << "\nLos Numeros Encontrados Fueron: \n";

	
	for (int i = 0; i < num_to_find_2; i++)
	{
		int found1 = -1;
		int found2 = -1;
		int finding = numToFind_2[i];
		int index = 0;

		for (int j = 0; j < num_to_find; j++)
		{
			index = j;
			if (numToFind[j] > finding) {
				
				break;
			}
		}
		if (index <= 0) {
			index++;
		}

		
		if (index + 1 >= num_to_find) {
			index = num_to_find - 2;
		}
		for (int j = index+1; j >= 0; j--) {
			if (numToFind[j] < finding) {
				found1 = numToFind[j];
				break;
			}
		}
		if (index - 1 < 0) {
			index = 1;
		}
		for (int j = index-1; j < num_to_find; j++) {
			if (numToFind[j] > finding) {
				found2 = numToFind[j];
				break;
			}
		}
		if (found1 < 0) {
			std::cout << "X" << " ";
		}
		else {
			std::cout << found1 << " ";
		}
		std::cout << " ";

		if (found2 < 0) {
			std::cout << "X" << " ";
		}
		else
		{
			std::cout << found2 << " ";
		}
		std::cout << "\n ";
		
	}
}

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
