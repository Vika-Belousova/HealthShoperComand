import React from 'react';
import { Box } from '@mui/material';
import { Swiper, SwiperSlide } from 'swiper/react';
import { Pagination, Navigation, Autoplay } from 'swiper/modules';

const slides = [
  {
    id: 1,
    image: '/common1.jpg',
  },
  {
    id: 2,
    image: '/common2.jpg',
  },
  {
    id: 3,
    image: '/common3.jpg',
  },
  {
    id: 4,
    image: '/common4.jpg',
  },
  {
    id: 5,
    image: '/common5.jpg',
  },
];

export const MainContent: React.FC = () => {
  return (
    <Swiper
      modules={[Pagination, Navigation, Autoplay]}
      pagination={{ clickable: true }}
      navigation
      autoplay={{ delay: 3500 }}
      style={{ maxWidth: '80vw', height: '420px' }}
      loop
    >
      {slides.map(slide => (
        <SwiperSlide key={slide.id}>
          <Box
            component="img"
            src={slide.image}
            alt={`slide-${slide.id}`}
            sx={{
              width: '100%',
              height: '100%',
              objectFit: 'cover',
              borderRadius: 2,
            }}
          />
        </SwiperSlide>
      ))}
    </Swiper>
  );
};
