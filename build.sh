# ensures that we always use the latest version of the script
if [ -f build-site.sh ]; then
  rm build-site.sh
fi 
curl https://raw.githubusercontent.com/mongodb/docs-worker-pool/DOP-5171-parser-cache/scripts/build-site.sh -o build-site.sh 
sh build-site.sh
