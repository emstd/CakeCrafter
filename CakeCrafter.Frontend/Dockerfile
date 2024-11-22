FROM node:20.18.0
WORKDIR /app
COPY . .
RUN npm install
RUN npm run build
CMD [ "npm", "run", "preview", "--", "--host" ]