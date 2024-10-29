#Stage 1: Build React
FROM node:20.18.0 as build
WORKDIR /app
COPY . .
RUN npm install
RUN npm run build

#Stage 2: Nginx
FROM nginx:alpine
COPY --from=build /app/dist /usr/share/nginx/html
COPY ./nginx.conf /etc/nginx/conf.d/default.conf