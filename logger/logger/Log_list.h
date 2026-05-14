#pragma once
#include <iostream>
using namespace std;

typedef struct Log_list {
	Log_list* next = nullptr;

	int day = 0;
	int year = 0;
	int time = 0;
	int type = -1;
	string filename = "";
	string filename1 = "";
	float ms = 0;
	bool err_stat = false;

	Log_list* data(Log_list* f, int year, int day, int time, int type, string filename, string filename1, int ms, bool err_stat);
	void insert(Log_list* uzel_add, Log_list* q);
	void progon(int year, int day, int time, int type, string filename, string filename1, int ms, bool err_stat);
	void init();
	void print();
	void delt(int year, int day, int time);
	void log_new();
	
};

typedef struct File_data {
	File_data* next = nullptr;
	string file = "";
	bool search(string file, File_data* perv);
	void rename(string file1, string file2, File_data* perv);
	void form();
	void delit(string file, File_data* perv);
	void file_check();
};


