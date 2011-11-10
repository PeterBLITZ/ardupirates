/*
 * median_filter.h
 *
 *  Created on: 28.09.2011
 *      Author: bouwerob
 */

#ifndef MEDIAN_FILTER_H_
#define MEDIAN_FILTER_H_

#define max_median_elements 5		// samples in the buffer

/**
 * data structure for the median filter
 */
typedef struct medianFilter
{
	uint8_t		index;
	uint16_t	samples[max_median_elements];
//	uint8_t		full;
} median_t;

// http://www.arduino.cc/playground/Main/MaxSonar
//Sorting function
// sort function (Author: Bill Gentles, Nov. 12, 2010)
static void isort(uint16_t *a, uint8_t n){
// *a is an array pointer function
 
  for (uint8_t i = 1; i < n; ++i)
  {
    uint16_t j = a[i];
    uint8_t k;
    for (k = i - 1; (k >= 0) && (j < a[k]); k--)
    {
      a[k + 1] = a[k];
    }
    a[k + 1] = j;
  }

 } 


static uint16_t middle_of_3(uint16_t a, uint16_t b, uint16_t c)
{
	uint16_t middle;

	if ((a <= b) && (a <= c))
	{
		middle = (b <= c) ? b : c;
	}
	else if ((b <= a) && (b <= c))
	{
		middle = (a <= c) ? a : c;
	}
	else
	{
		middle = (a <= b) ? a : b;
	}
	return middle;
}

static uint16_t getMedian(median_t *filterChannel)
{
	if ( filterChannel->index > 3)
	{
		filterChannel->index = 0;
		//filterChannel->full = 0; // reset full indicator
		isort(filterChannel->samples, max_median_elements);
		return filterChannel->samples[max_median_elements/2];
	}
	else
	{
		filterChannel->index = 0;
		//filterChannel->full = 0; // reset full indicator
		return middle_of_3(filterChannel->samples[0], filterChannel->samples[1], filterChannel->samples[2]);
	}
/***
	if (filterChannel->full)
	{
		filterChannel->full = 0; // reset full indicator
		return middle_of_3(filterChannel->samples[0], filterChannel->samples[1], filterChannel->samples[2]);
	}
	else
	{
		return 0;
	}
***/
}

static void initMedianFilter(median_t *filterChannel)
{
	uint8_t i;
	filterChannel->index = 0;
	//filterChannel->full = 0;
	for (i = 0; i < max_median_elements; i++)
	{
		filterChannel->samples[i] = 0;
	}
}

static void addMedianSample(median_t *filterChannel, uint16_t sample)
{
	// add previous value if the current value is zero
	filterChannel->samples[filterChannel->index++] = sample;
	if (filterChannel->index >= max_median_elements)
	{
		//filterChannel->full = 1; // set full indicator
		filterChannel->index = 0; // reset index
	}
}

#endif /* MEDIAN_FILTER_H_ */
