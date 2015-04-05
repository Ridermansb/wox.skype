Write-Host -ForegroundColor Cyan "Generate coverage report ..."

git clone -b -n --depth=1 gh-pages "https://github.com/Ridermansb/wox.skype.git" gh-pages

ls

cd gh-pages

. ..\$env:reportPath  -reports:..\$env:coverResultFile -targetdir:coverage



Write-Host -ForegroundColor Cyan "Upload coverage report ..."

git add .

git commit -am "Update coverage code report"

git push origin gh-pages
