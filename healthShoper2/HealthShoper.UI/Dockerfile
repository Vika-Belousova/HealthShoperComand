# ---------- 1. Build stage ----------
FROM node:20.19.0-alpine AS build

WORKDIR /app

# Устанавливаем версии npm такие же, как у тебя в package.json
RUN npm install -g npm@10.8.2

# Копируем package.json и lock
COPY package.json package-lock.json ./

# Устанавливаем зависимости (кэшируется при изменении только package.json)
RUN npm ci

# Копируем все исходники
COPY . .

# Собираем TypeScript
RUN npm run build

# ---------- 2. Nginx stage ----------
FROM nginx:stable-alpine

# Удаляем дефолтный конфиг
RUN rm /etc/nginx/conf.d/default.conf

# Кладём наш конфиг
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Копируем билд из предыдущего контейнера
COPY --from=build /app/dist /usr/share/nginx/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
