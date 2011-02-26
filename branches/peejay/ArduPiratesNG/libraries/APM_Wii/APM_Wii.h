#ifndef APM_Wii_h
#define APM_Wii_h


class APM_Wii
{
  private:
	void FetchData();

  public:
	APM_Wii();  // Constructor
	void Init();
	int Ch(unsigned char ch_num);
};

#endif