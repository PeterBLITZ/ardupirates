#ifndef APM_Wii_h
#define APM_Wii_h


class APM_Wii_Class
{
  private:
	void FetchData();
  public:
	APM_Wii_Class();  // Constructor
	void Init();
	int Ch(unsigned char ch_num);     
};

extern APM_Wii_Class APM_Wii;

#endif