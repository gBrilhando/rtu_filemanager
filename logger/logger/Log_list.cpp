#include "Log_list.h"
#include <iostream>
#include <fstream>
#include <iomanip>
#include <string>
#include <windows.h>
#include <locale.h>

Log_list* first = nullptr; // Адрес первого узла списка автомобилей храним тут
Log_list* temp_obj = nullptr;
File_data* add = nullptr;
File_data* perv = nullptr;

Log_list* Log_list::data(Log_list* f, int year, int day, int time, int type, string filename, string filename1, int ms, bool err_stat) { // Добавление данных в узел
	f = new Log_list;
	f->year = year;
	f->day = day;
	f->time = time;
	f->type = type;
	f->filename = filename;
	f->filename1 = filename1;
	f->ms = ms;
	f->err_stat = err_stat;
	return (f);
}

void Log_list::insert(Log_list* uzel_add, Log_list* q)
{
	Log_list* p = uzel_add->next;
	uzel_add->next = q;
	if (p) q->next = p;
	return;
}

void Log_list::progon(int year, int day, int time, int type, string filename, string filename1, int ms, bool err_stat)
{
	if (!first) { // Если первого элемента в списке нет, добавляем его
		first = data(first, year, day, time, type, filename, filename1, ms, err_stat);
		return;
	}
	temp_obj = data(temp_obj, year, day, time, type, filename, filename1, ms, err_stat); // Сначала вводим данные для временного узла
	if ((temp_obj->year < first->year) || ((temp_obj->year == first->year)&&(temp_obj->day < first->day)) || ((temp_obj->year == first->year)&&(temp_obj->day == first->day)&&(temp_obj->time < first->time))) // Обязательно сравниваем время лога. Если узел прошел проверку сразу, он становится первым
	{
		temp_obj->next = first;
		first = temp_obj;
		return;
	}
	else { // Если узел не прошел проверку, продолжаем "прогон" по списку до победного
		Log_list* uzel_back = first;
		Log_list* uzel_add = first;
		while (uzel_back) {
			if ((temp_obj->year > uzel_back->year) || ((temp_obj->year == uzel_back->year) && (temp_obj->day > uzel_back->day)) || ((temp_obj->year == uzel_back->year) && (temp_obj->day == uzel_back->day) && (temp_obj->time > uzel_back->time))) {
				uzel_add = uzel_back;
				uzel_back = uzel_back->next;
			}
			else {
				insert(uzel_add, temp_obj);
				return;
			}
		}
		insert(uzel_add, temp_obj);
		return;
	}

}

void Log_list::init() { //Одно из заданий лабораторной работы - инициализация списка. Инициализация выполняется этой функцией
		string line;
		ifstream in("D:\\МИРЭА\\Операционные системы\\OS_File_4\\log.txt");
		if (!in) {}
		else {
			while (getline(in, line)) {
				string razdel = " ";
				string ender = ".txt";
				string skobka = ")";
				int day = stoi(line.substr(0)) + stoi(line.substr(3)) * 31;
				int year = stoi(line.substr(6));
				string line1 = line.substr(0, line.find(razdel));
				line = line.substr(line1.size() + 1);
				int time = stoi(line.substr(0)) * 3600;
				line = line.substr(line.find(":"));
				time+=stoi(line.substr(1)) * 60;
				line = line.substr(1);
				line = line.substr(line.find(":"));
				time+=stoi(line.substr(1));
				line1 = line.substr(0, line.find(razdel));
				line = line.substr(line1.size() + 1);
				line1 = line.substr(0, line.find(razdel));
				line = line.substr(line1.size() + 1);
				line1 = line.substr(0, line.find(razdel));
				int type = 0;
				if (line1 == "created") type = 0;
				if (line1 == "renamed") type = 1;
				if (line1 == "read") type = 2;
				if (line1 == "changed") type = 3;
				if (line1 == "deleted") type = 4;

				line = line.substr(line1.size() + 1);
				string filename = line.substr(0, line.find(ender));
				string filename1 = "";
				if (type != 1) {
					line1 = line.substr(filename.size() + 5);
				}
				else {
					line1 = line.substr(filename.size() + 8);
					filename1 = line1.substr(0, line1.find(ender));
					line1 = line1.substr(filename1.size() + 5);
				}
				int ms = stoi(line1);
				line = line1.substr(to_string(ms).size() + 3);
				bool err_stat = false;
				if (line == " - err") err_stat = true;
				progon(year, day, time, type, filename, filename1, ms, err_stat);
			}
		}
}

void Log_list::print() { //Выводи список машин
	Log_list* uzel = first;
	while (uzel) {
		if (uzel == first) cout << "Начало списка:" << endl;
		cout << "Год:   " << uzel->year << endl;
		cout << "День:  " << uzel->day << endl;
		cout << "Время: " << uzel->time << endl;
		cout << "Тип:   " << uzel->type << endl;
		cout << "Файл:  " << uzel->filename << endl;
		if (uzel->type == 1) cout << "Файл 2:  " << uzel->filename1 << endl;
		if (uzel->err_stat) cout << "Прерван" << endl;
		cout << "________________________" << endl;
		uzel = uzel->next;
	}
}

bool File_data::search(string file, File_data* perv) {
	File_data* temp_obj = perv;
	if (temp_obj == nullptr) return false;
	while (temp_obj) {
		if (temp_obj->file == file) return true;
        temp_obj = temp_obj->next;
	}
	return false;
}

void Log_list::delt(int year, int day, int time) // Удаляем элементы списка относительно марки
{
	Log_list* temp_obj = first;
	Log_list* temp_back = nullptr;
	while (temp_obj) {
		if ((temp_obj->year == year) && (temp_obj->day == day) && (temp_obj->time == time)) {
			if (temp_obj == first) {
				temp_back = first;
				first = temp_obj->next;
				temp_obj = temp_obj->next;
			}
			else {
				if (!temp_back) temp_back = first;
				temp_back->next = temp_obj->next;
				temp_obj = temp_obj->next;
			}
		}
		else {
			temp_obj = temp_obj->next;
			if (!temp_back) temp_back = first;
			else temp_back = temp_back->next;
		}
	}
}

void File_data::rename(string file1, string file2, File_data* perv) {
	File_data* list = perv;
	while (list) {
		if (list->file == file1) {
			list->file = file2;
			return;
		}
		list = list->next;
	}
}

void File_data::delit(string file, File_data* perv) // Удаляем элементы списка относительно марки
{
	File_data* temp_obj = perv;
	File_data* temp_back = nullptr;
	while (temp_obj) {
		if (temp_obj->file == file) {
			if (temp_obj == perv) {
				temp_back = perv;
				perv = temp_obj->next;
				temp_obj = temp_obj->next;
			}
			else {
				if (!temp_back) temp_back = perv;
				temp_back->next = temp_obj->next;
				temp_obj = temp_obj->next;
			}
		}
		else {
			temp_obj = temp_obj->next;
			if (!temp_back) temp_back = perv;
			else temp_back = temp_back->next;
		}
	}
}

void File_data::file_check()
{
	File_data* temp_obj = perv;
	if (temp_obj == nullptr) return;
	WIN32_FIND_DATAA findData;
	HANDLE hf;
	setlocale(0, "");
	hf = FindFirstFileA("D:\\МИРЭА\\Операционные системы\\OS_File_4\\Files\\*.txt", &findData);
	if (hf == INVALID_HANDLE_VALUE)
	{
	}
	do
	{
		string a = findData.cFileName;
		string b = a.substr(0, a.find("."));
		bool ch = false;
		temp_obj = perv;
		while (temp_obj) {
			if (temp_obj->file == b) ch = true;
			temp_obj = temp_obj->next;
		}
		if (!ch) {
			string c = "D:\\МИРЭА\\Операционные системы\\OS_File_4\\Files\\" + a;
			int cs = c.length();
			char* ca = new char[cs+1];
			strcpy_s(ca,cs+1,c.c_str());
			remove(ca);
			cout << a << " удален" << endl;
		}
	} while (FindNextFileA(hf, &findData));
	FindClose(hf);
	temp_obj = perv;
	while (temp_obj) {
		string a = temp_obj->file + ".txt";
		string c = "D:\\МИРЭА\\Операционные системы\\OS_File_4\\Files\\" + a;
		int cs = c.length();
		char* ca = new char[cs + 1];
		strcpy_s(ca, cs + 1, c.c_str());
		fstream in(ca);
		if (!in.is_open()) {
			ofstream in1(ca);
			in1.close();
			cout << "Файл " << a << " создан" << endl;
		}
		temp_obj = temp_obj->next;
	}
	temp_obj = perv;
	while (temp_obj) {
		cout << temp_obj->file << endl;
		temp_obj = temp_obj->next;
	}
	return;
}

void File_data::form() {
	Log_list* list = first;
	while (list) {
		switch (list->type) {
		case 0:
			if (list->err_stat) {
				cout << "Создание " << list->filename << " прервано. Продолжить? (y/n): ";
				string a;
				cin >> a;
				if (a == "y") {
					list->err_stat = false;
					break;
				}
				else {
					Log_list* temp = list->next;
					list->delt(list->year, list->day, list->time);
					list = temp;
					break;
				}
			}
			if (!search(list->filename, perv)) {
				if (!add) {
					add = new File_data;
					perv = add;
				}
				else {
					add->next = new File_data;
					add = add->next;
				}
			add->file = list->filename;
			list = list->next;
			break;
		}
			else {
				Log_list* temp = list->next;
				list->delt(list->year, list->day, list->time);
				list = temp;
				break;
			}
		case 1:
			if (list->err_stat) {
				cout << "Переименование " << list->filename <<  " на " << list->filename1 << " прервано. Создать новый файл? (y/n): ";
				string a;
				cin >> a;
				if (a == "y") {
					list->filename = list->filename1;
					list->filename1 = "";
					list->err_stat = false;
					break;
				}
				else {
					Log_list* temp = list->next;
					list->delt(list->year, list->day, list->time);
					list = temp;
					break;
				}
			}
			if (!search(list->filename, perv)) {
				list->type = 0;
				list->filename = list->filename1;
				list->filename1 = "";
				break;
			}
			if (search(list->filename1, perv)) {
				Log_list* temp = list->next;
				list->delt(list->year, list->day, list->time);
				list = temp;
				break;
			}
			else {
				rename(list->filename, list->filename1, perv);
				list = list->next;
				break;
			}
		case 2:
			if (list->err_stat) {
					Log_list* temp = list->next;
					list->delt(list->year, list->day, list->time);
					list = temp;
					break;
			}
			if (!search(list->filename, perv)) {
				Log_list* temp = list->next;
				list->delt(list->year, list->day, list->time);
				list = temp;
				break;
			}
			else {
				list = list->next;
				break;
			}
		case 3:
			if (list->err_stat) {
				Log_list* temp = list->next;
				list->delt(list->year, list->day, list->time);
				list = temp;
				break;
			}
			if (!search(list->filename, perv)) {
				Log_list* temp = list->next;
				list->delt(list->year, list->day, list->time);
				list = temp;
				break;
			}
			else {
				list = list->next;
				break;
			}
		case 4:
			if (list->err_stat) {
				cout << "Удаление " << list->filename << " прервано. Продолжить? (y/n): ";
				string a;
				cin >> a;
				if (a == "y") {
					list->err_stat = false;
					break;
				}
				else {
					Log_list* temp = list->next;
					list->delt(list->year, list->day, list->time);
					list = temp;
					break;
				}
			}
			if (!search(list->filename, perv)) {
				Log_list* temp = list->next;
				list->delt(list->year, list->day, list->time);
				list = temp;
				break;
			}
			else {
				if (add->next) {
					File_data* temp = add->next;
					add->delit(list->filename, perv);
					add = temp;
					list = list->next;
					break;
				}
				else {
					File_data* temp = perv;
					bool status = false;
					while (temp) {
						if ((temp == perv) && (temp->file == list->filename)) {
							perv == nullptr;
							return;
						}
						else {
							if (temp->next->file == list->filename) {
								temp->next = temp->next->next;
								status = true;
								break;
							}
						}
						temp = temp->next;
						if (status) break;
					}
					list = list->next;
					break;
				}
			}
		default:
			Log_list* temp = list->next;
			list->delt(list->year, list->day, list->time);
			list = temp;
			break;
		}
	}
}

string forma(int a) {
	string c = to_string(a);
	if (a / 10 == 0) return "0" + c;
	else return c;
}

void Log_list::log_new(){
	ofstream file;
	file.open("D:\\МИРЭА\\Операционные системы\\OS_File_4\\log.txt", ios::out);
	Log_list* temp_obj = first;
	while (temp_obj) {
		file << forma(temp_obj->day % 31) << "." << forma(temp_obj->day / 31) << "." << temp_obj->year << " ";
		file << forma(temp_obj->time /3600) << ":" << forma(temp_obj->time / 60 % 60) << ":" << forma(temp_obj->time % 60) << " ";
		file << "user ";
		switch (temp_obj->type) {
		case 0: file << "created "; break;
		case 1: file << "renamed "; break;
		case 2: file << "read "; break;
		case 3: file << "changed "; break;
		case 4: file << "deleted "; break;
		}
		if (temp_obj->type == 1) file << temp_obj->filename << ".txt to " << temp_obj->filename1 << ".txt";
		else file << temp_obj->filename << ".txt";
		file << "(" << temp_obj->ms << " ms)"<< endl;
		temp_obj = temp_obj->next;
	}
}
